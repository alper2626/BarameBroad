using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using BaseEntities.Abstract;
using BaseEntities.Concrete;
using Core.Core;
using Core.DataAccess.Abstract;
using Core.LogModule.Jobs;
using Core.ServiceInjector.Utilities.IoC;
using Core.WebHelper;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.Concrete
{
    public class EntityRepositoryBase<TEntity, TSub> : IEntityRepositoryBase<TEntity, TSub>
        where TEntity : class, IEntity, new()
        where TSub : LangEntity<TEntity>, new()
    {
        public TEntity Get(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[] includes = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                IQueryable<TEntity> query = context.Set<TEntity>();

                if (includes != null)
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                return query.FirstOrDefault(filter);
            }
        }


        public TEntity CreateAndGet(Expression<Func<TEntity, bool>> filter, TEntity ent, Expression<Func<TEntity, object>>[] includes = null)
        {
            var newEnt = this.Get(filter, includes);

            if (newEnt != null)
            {
                return newEnt;
            }
            else if (this.CreateWithSubEntities(ent))
            {
                return ent;
            }

            return null;
        }

        public TEntity First(Expression<Func<TEntity, object>>[] includes = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                IQueryable<TEntity> query = context.Set<TEntity>();

                if (includes != null)
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                return query.FirstOrDefault();
            }
        }

        public TEntity GetLast(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                IQueryable<TEntity> query = context.Set<TEntity>();


                if (includes != null)
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                return filter == null ?
                 query.OrderByDescending(w => w.Id).FirstOrDefault() :
                 query.OrderByDescending(w => w.Id).FirstOrDefault(filter);
            }
        }


        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                IQueryable<TEntity> query = context.Set<TEntity>();

                if (includes != null)
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                return filter != null ? query.Where(filter).ToList() : query.ToList();
            }
        }

        public IEnumerable<TEntity> GetListSkipTake(int skip, int take, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                IQueryable<TEntity> query = context.Set<TEntity>().OrderBy(s => s.Id);

                if (includes != null)
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                return filter != null ? query.Where(filter).Skip(skip).Take(take).ToList() : query.Skip(skip).Take(take).ToList();
            }
        }


        public bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                return filter != null ? context.Set<TEntity>().Any(filter) : context.Set<TEntity>().Any();
            }
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                return filter != null ? context.Set<TEntity>().Count(filter) : context.Set<TEntity>().Count();
            }
        }

        public bool SetState(TEntity entity, EntityState state)
        {

            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                context.Entry(entity).State = state;
                UpdateDateValues(entity, state, context);
                return context.SaveChanges() > 0;
            }
        }

        public TEntity SetStateEntity(TEntity entity, EntityState state)
        {

            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                context.Entry(entity).State = state;
                UpdateDateValues(entity, state, context);
                context.SaveChanges();
                return entity;
            }
        }

        public List<TEntity> SetStateEntity(List<TEntity> entity, EntityState state)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                context.Entry(entity).State = state;
                context.SaveChanges();
                return entity;
            }
        }
        public bool SetStateEntity(CreateEntityModel<TEntity, TSub> createEntityModel, EntityState state)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (CustomDbContext context = new CustomDbContext())
                    {
                        context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                        context.Entry(createEntityModel.Entity).State = state;
                        UpdateDateValues(createEntityModel.Entity, state, context);

                        foreach (var item in createEntityModel.LangEntities)
                        {
                            context.Entry(item).State = state;
                            UpdateDateValues(item, state, context);
                        }
                        context.SaveChanges();
                        scope.Complete();
                        return true;
                    }
                }
                catch (Exception)
                {
                    scope.Dispose();
                    return false;
                }

            }
        }

        public bool CreateWithSubEntities(TEntity entity)
        {
            try
            {
                using (var context = new CustomDbContext())
                {
                    UpdateDateValues(entity, EntityState.Added, context);
                    context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                    context.Set<TEntity>().Add(entity);
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool UpdateWithSubEntities(TEntity entity)
        {
            using (var context = new CustomDbContext())
            {
                UpdateDateValues(entity, EntityState.Modified, context);

                context.Set<TEntity>().Update(entity);
                return context.SaveChanges() > 0;
            }
        }

        protected void UpdateDateValues(IEntity entity, EntityState state, CustomDbContext context)
        {
            if (state == EntityState.Added)
            {
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
            }
            else if (state == EntityState.Modified)
            {
                entity.UpdateTime = DateTime.Now;
                context.Entry(entity).Property(w => w.CreateTime).IsModified = false;
            }
        }

        public virtual List<SiteMapEntity> CreateSiteMap()
        {
            return new List<SiteMapEntity>();
        }

    }



    public class EntityRepositoryBase<TEntity> : IEntityRepositoryBase<TEntity>
        where TEntity : class, IEntity, new()
    {
        public TEntity Get(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[] includes = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();

                if (includes != null)
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                return query.AsNoTracking().FirstOrDefault(filter);
            }
        }

        public TEntity First(Expression<Func<TEntity, object>>[] includes = null)
        {

            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                
                IQueryable<TEntity> query = context.Set<TEntity>();

                if (includes != null)
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                return query.FirstOrDefault();
                
            }
        }

        public TEntity GetLast(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                IQueryable<TEntity> query = context.Set<TEntity>();


                if (includes != null)
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                return filter == null ?
                 query.OrderByDescending(w => w.Id).FirstOrDefault() :
                 query.OrderByDescending(w => w.Id).FirstOrDefault(filter);
            }
        }


        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                IQueryable<TEntity> query = context.Set<TEntity>();

                if (includes != null)
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                return filter != null ? query.Where(filter).ToList() : query.ToList();
            }
        }

        public IEnumerable<TEntity> GetListSkipTake(int skip, int take, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                IQueryable<TEntity> query = context.Set<TEntity>().OrderBy(s => s.Id);

                if (includes != null)
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                return filter != null ? query.Where(filter).OrderByDescending(w => w.CreateTime).Skip(skip).Take(take).ToList() : query.OrderByDescending(w => w.CreateTime).Skip(skip).Take(take).ToList();
            }
        }


        public bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                return filter != null ? context.Set<TEntity>().Any(filter) : context.Set<TEntity>().Any();
            }
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                return filter != null ? context.Set<TEntity>().Count(filter) : context.Set<TEntity>().Count();
            }
        }

        public bool SetState(TEntity entity, EntityState state)
        {

            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                context.Entry(entity).State = state;
                UpdateDateValues(entity, state, context);
                return context.SaveChanges() > 0;
            }
        }

        public TEntity SetStateEntity(TEntity entity, EntityState state)
        {

            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                context.Entry(entity).State = state;
                UpdateDateValues(entity, state, context);
                context.SaveChanges();
                return entity;
            }
        }

        public List<TEntity> SetStateEntity(List<TEntity> entity, EntityState state)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                foreach (var item in entity)
                {
                    context.Entry(item).State = state;
                    UpdateDateValues(item, state, context);
                }
                context.SaveChanges();
                return entity;
            }

        }

        public bool CreateWithSubEntities(TEntity entity)
        {

            using (var context = new CustomDbContext())
            {
                UpdateDateValues(entity, EntityState.Added, context);
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                context.Set<TEntity>().Add(entity);
                return context.SaveChanges() > 0;
            }
        }

        public bool UpdateWithSubEntities(TEntity entity)
        {
            using (var context = new CustomDbContext())
            {
                UpdateDateValues(entity, EntityState.Modified, context);
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                context.Set<TEntity>().Update(entity);
                return context.SaveChanges() > 0;
            }
        }

        protected void UpdateDateValues(IEntity entity, EntityState state, CustomDbContext context)
        {
            if (state == EntityState.Added)
            {
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
            }
            else if (state == EntityState.Modified)
            {
                entity.UpdateTime = DateTime.Now;
                context.Entry(entity).Property(w => w.CreateTime).IsModified = false;
            }
        }


        public virtual List<SiteMapEntity> CreateSiteMap()
        {
            return new List<SiteMapEntity>();
        }

        public bool SetState(List<TEntity> entity, EntityState state)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                context.Entry(entity).State = state;
                return context.SaveChanges() > 0;
            }
        }
    }
}