using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BaseEntities;
using BaseEntities.Abstract;
using BaseEntities.Concrete;
using Core.Core;
using Core.DataAccess.Abstract;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Extensions.AutoMapperExtensions;
using Extensions.DevExtremeQueryableExtension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;

namespace Core.DataAccess.Concrete
{
    public class QueryableRepositoryBase<T> : IQueryableRepositoryBase<T> where T : class, IEntity, new()
    {
        public DbSet<T> Table => GetContext().Set<T>();

        private DbContext GetContext()
        {
            var a = new CustomDbContext();
            a.Database.SetCommandTimeout(1000000000);
            return new CustomDbContext();
        }

        public void Dispose(DbSet<T> dbSet)
        {
            var context = dbSet.GetService<ICurrentDbContext>().Context;
            context.Dispose();
        }

        public string DxGridList<TMapTo>(DataSourceLoadOptions loadOptions, DbSet<T> queryable)
            where TMapTo : class, new()
        {
            var result = JsonConvert.SerializeObject(queryable.BindOptionMapped<T, TMapTo>(loadOptions));
            Dispose(queryable);
            return result;
        }

        public string DxQueryableGridList<TMapTo>(DataSourceLoadOptions loadOptions, IQueryable<T> queryable, DbSet<T> dbSet) where TMapTo : class, new()
        {
            var result = JsonConvert.SerializeObject(queryable.BindOptionMapped<T, TMapTo>(loadOptions), Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            Dispose(dbSet);
            return result;
        }

        public async Task<List<ProgramSpResponse>> ExecuteListSpMsSqlAsync(string spName, Hashtable parameters)
        {
            try
            {

                List<ProgramSpResponse> turnValue = new List<ProgramSpResponse>();
                using (SqlConnection con = new SqlConnection(GetContext().Database.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (var item in parameters.Keys)
                        {
                            var value = parameters[item];
                            cmd.Parameters.Add($"@{item}", DbTypeConverter.ToSqlDbType(value.GetType())).Value = value;
                        }
                        con.Open();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                turnValue.Add(new ProgramSpResponse
                                {
                                    Name = reader.GetString("Link"),
                                    Link = reader.GetString("Name"),
                                    EntityRelationType = reader.GetInt16("EntityRelationType")
                                });
                            }
                        }

                        reader.Close();

                    }
                }
                return turnValue;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}