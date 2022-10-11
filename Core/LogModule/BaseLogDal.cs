using System.Collections.Generic;
using Core.Core;
using Core.LogModule.Entities;

namespace Core.LogModule
{
    public class BaseLogDal : IBaseLogDal
    {
        public void Log(Log log)
        {
            using (var context = new CustomDbContext())
            {
                context.Set<Log>().Add(log);
                context.SaveChanges();
            }
        }

        public void MultiLog(List<Log> log)
        {
            using (var context = new CustomDbContext())
            {
                context.Set<Log>().AddRange(log);
                context.SaveChanges();
            }
        }
    }
}