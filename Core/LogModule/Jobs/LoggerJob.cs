using System;
using System.Collections.Generic;
using BaseEntities.Abstract;
using BaseEntities.EnumTypes;
using Core.LogModule.Entities;
using Extensions.EntityAttributeExtensions;
using Extensions.EnumExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Core.LogModule.Jobs
{
    public static class LoggerJob
    {
        [Obsolete]
        public static void Log(IEntity entity, EntityState state, string userFullName)
        {
            
            OperationType ot = OperationType.Create;
            switch (state)
            {
                case EntityState.Detached:
                    ot = OperationType.Create;
                    break;
                case EntityState.Unchanged:
                    ot = OperationType.Create;
                    break;
                case EntityState.Deleted:
                    ot = OperationType.Delete;
                    break;
                case EntityState.Modified:
                    ot = OperationType.Update;
                    break;
                case EntityState.Added:
                    ot = OperationType.Create;
                    break;
            }

            Hangfire.BackgroundJob.Enqueue<IBaseLogDal>(job => job.Log(CreateLogObj(entity, ot, userFullName, "")));
        }

        [Obsolete]
        public static void OperationLog(IEntity entity, OperationType type, string userFullName)
        {
            
            Hangfire.BackgroundJob.Enqueue<IBaseLogDal>(job => job.Log(CreateLogObj(entity, type, userFullName, "")));
        }

        [Obsolete]
        public static void MultiLog(List<IEntity> entities, EntityState state, string userFullName)
        {
            
            OperationType ot = OperationType.Create;
            switch (state)
            {
                case EntityState.Detached:
                    ot = OperationType.Create;
                    break;
                case EntityState.Unchanged:
                    ot = OperationType.Create;
                    break;
                case EntityState.Deleted:
                    ot = OperationType.Delete;
                    break;
                case EntityState.Modified:
                    ot = OperationType.Update;
                    break;
                case EntityState.Added:
                    ot = OperationType.Create;
                    break;
            }
            List<Log> logs = new List<Log>();
            foreach (var entity in entities)
                logs.Add(CreateLogObj(entity, ot, userFullName));

            Hangfire.BackgroundJob.Enqueue<IBaseLogDal>(job => job.MultiLog(logs));
        }

        [Obsolete]
        public static void CustomLog(IEntity entity, EntityState state, string userFullName, string description)
        {
            
            OperationType ot = OperationType.Create;
            switch (state)
            {
                case EntityState.Detached:
                    ot = OperationType.Create;
                    break;
                case EntityState.Unchanged:
                    ot = OperationType.Create;
                    break;
                case EntityState.Deleted:
                    ot = OperationType.Delete;
                    break;
                case EntityState.Modified:
                    ot = OperationType.Update;
                    break;
                case EntityState.Added:
                    ot = OperationType.Create;
                    break;
            }

            Hangfire.BackgroundJob.Enqueue<IBaseLogDal>(job => job.Log(CreateLogObj(entity, ot, userFullName, description)));
        }

        private static Log CreateLogObj(IEntity entity, OperationType type, string userFullName, string description = "")
        {
            var entType = entity.GetType();
            var log = new Log
            {
                Data = JsonConvert.SerializeObject(entity),
                CreateTime = DateTime.Now,
                DataRef = entity.Id,
                OperationType = type,
                UserFullName = userFullName,
                Description = description,
                TableName = entity.GetType().Name,
                TableDisplayName = entity.GetDescription(),
                TypeStr = $"{entType.FullName},{entType.Assembly.GetName().Name}"
            };
            if (string.IsNullOrEmpty(log.Description))
                log.Description =
                    $"{log.UserFullName} {log.TableDisplayName} Tablosunda {log.OperationType.GetDescription()} İşlemi Gerçekleştirdi.";
            return log;
        }
    }
}