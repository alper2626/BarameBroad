using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BaseEntities.Abstract;
using BaseEntities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.Abstract
{
    public interface IEntityRepositoryBase<T,TSub>
        where T : class, IEntity, new()
        where TSub : LangEntity<T>, new()
    {
        T Get(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includes = null);

        T CreateAndGet(Expression<Func<T, bool>> filter, T ent, Expression<Func<T, object>>[] includes = null);

        T First(Expression<Func<T, object>>[] includes = null);

        T GetLast(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>>[] includes = null);

        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>>[] includes = null);

        IEnumerable<T> GetListSkipTake(int skip, int take, Expression<Func<T, bool>> filter = null, Expression<Func<T, object>>[] includes = null);

        bool Any(Expression<Func<T, bool>> filter = null);

        int Count(Expression<Func<T, bool>> filter = null);

        bool SetState(T entity, EntityState state);

        T SetStateEntity(T entity, EntityState state);

        bool SetStateEntity(CreateEntityModel<T,TSub> createEntityModel, EntityState state);

        List<T> SetStateEntity(List<T> entity, EntityState state);

        bool CreateWithSubEntities(T entity);

        bool UpdateWithSubEntities(T entity);

        List<SiteMapEntity> CreateSiteMap();

    }

    public interface IEntityRepositoryBase<T>
    where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includes = null);

        T First(Expression<Func<T, object>>[] includes = null);

        T GetLast(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>>[] includes = null);

        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>>[] includes = null);

        IEnumerable<T> GetListSkipTake(int skip, int take, Expression<Func<T, bool>> filter = null, Expression<Func<T, object>>[] includes = null);

        bool Any(Expression<Func<T, bool>> filter = null);

        int Count(Expression<Func<T, bool>> filter = null);

        bool SetState(T entity, EntityState state);

        bool SetState(List<T> entity, EntityState state);

        T SetStateEntity(T entity, EntityState state);

        List<T> SetStateEntity(List<T> entity, EntityState state);

        bool CreateWithSubEntities(T entity);

        bool UpdateWithSubEntities(T entity);

        List<SiteMapEntity> CreateSiteMap();

    }
}