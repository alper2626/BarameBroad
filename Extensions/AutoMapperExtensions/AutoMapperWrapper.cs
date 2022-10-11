using System;
using System.Linq;
using AutoMapper;

namespace Extensions.AutoMapperExtensions
{
    public class AutoMapperWrapper
    {
        private static IMapper _mapper;
        private static object _lock = new object();

        public static IMapper Mapper
        {
            get
            {
                lock (_lock)
                {
                    if (_mapper == null)
                        _mapper = CreateConfiguration().CreateMapper();
                    return _mapper;
                }

            }
        }

        private static MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                var profiles = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes().Where(type => typeof(Profile).IsAssignableFrom(type)));
                cfg.AddMaps(profiles);
            });
            return config;
        }

    }
}