using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Extensions.AutoMapperExtensions;
using Microsoft.EntityFrameworkCore;

namespace Extensions.DevExtremeQueryableExtension
{
    public static class BindOptions
    {
        public static LoadResult BindOption<T>(this IQueryable<T> query, DataSourceLoadOptions options, Expression<Func<T, object>>[] includes = null)
        where T : class, new()
        {
            if (includes != null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            var result = DataSourceLoader.Load(query, options);
            return result;
        }

        public static LoadResult BindOptionMapped<T, TMapTo>(this IQueryable<T> query, DataSourceLoadOptions options,Expression<Func<T, object>>[] includes = null)
            where T : class, new()
            where TMapTo : class, new()
        {
            
            if (includes != null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            var response = DataSourceLoader.Load(query, options);
            
            if (options.Group != null)
                return response;
            
            return response;
            
        }


        public static IEnumerable<T> BindOptionGetData<T>(this IQueryable<T> query, DataSourceLoadOptions options)
            where T : class, new()
        {
            options.Group = null;
            return BindOption(query, options).data.Cast<T>();
        }


        public static IEnumerable<TMapTo> BindOptionGetDataMapped<T, TMapTo>(this IQueryable<T> query, DataSourceLoadOptions options)
            where T : class, new()
            where TMapTo : class, new()
        {
            options.Group = null;
            return BindOptionMapped<T, TMapTo>(query, options).data.Cast<TMapTo>();
        }

        public static int BindOptionGetOnlyCount<T>(this IQueryable<T> query, DataSourceLoadOptions options)
        {
            options.IsCountQuery = true;
            var response = DataSourceLoader.Load(query, options);
            return response.totalCount;
        }
    }
}