using System;
using System.Linq;
using System.Linq.Expressions;

namespace BaseEntities.Abstract
{
    public interface IFilterModel<T> : IPostModel
        where T : class, IEntity, new()
    {
        int Page { get; set; }

        int Skip => Take * Page;

        int Take { get; set; }

        bool UsePaging { get; set; }

        IQueryable<T> ApplyFilters(IQueryable<T> queryable);

        IQueryable<T> ApplyPageFilter(IQueryable<T> queryable);

        string CreateQueryFromModel();
    }
}