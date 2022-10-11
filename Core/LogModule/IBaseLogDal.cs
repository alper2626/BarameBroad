using System.Collections.Generic;
using Core.LogModule.Entities;

namespace Core.LogModule
{
    public interface IBaseLogDal
    {
        void Log(Log log);

        void MultiLog(List<Log> log);
    }
}