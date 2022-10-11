using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseEntities;
using BaseEntities.Abstract;
using DevExtreme.AspNet.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.Abstract
{
    public interface IQueryableRepositoryBase<T>  where T : class, IEntity, new()
    {
        DbSet<T> Table { get; }

        void Dispose(DbSet<T> dbSet);

        string DxGridList<TMapTo>(DataSourceLoadOptions loadOptions, DbSet<T> queryable)
            where TMapTo : class, new();

        string DxQueryableGridList<TMapTo>(DataSourceLoadOptions loadOptions, IQueryable<T> queryable,DbSet<T> dbSet)
            where TMapTo : class, new();

        Task<List<ProgramSpResponse>> ExecuteListSpMsSqlAsync(string spName, Hashtable parameters);

    }
}