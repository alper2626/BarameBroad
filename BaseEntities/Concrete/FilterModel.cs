using System;
using System.Linq;
using System.Linq.Expressions;
using BaseEntities.Abstract;
using CrossCuttingConcerns.Attributes;

namespace BaseEntities.Concrete
{
    public abstract class FilterModel<T> : IFilterModel<T>
        where T : class, IEntity, new()
    {
        public int Page
        {
            get => _page < 1 ? 1 : _page;

            set => _page = value;
        }

        public int Skip => Take * (Page - 1);

        [Filterable]
        public int Take { get; set; } = 4;

        public bool UsePaging { get; set; } = true;

        private int _page;

        public abstract IQueryable<T> ApplyFilters(IQueryable<T> queryable);


        public IQueryable<T> ApplyPageFilter(IQueryable<T> queryable)
        {
            if (UsePaging)
                queryable = queryable.Skip(Skip).Take(Take);

            return queryable;
        }

        public virtual string CreateQueryFromModel()
        {
            return "";
        }
    }
}