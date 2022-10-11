using Castle.DynamicProxy;
using CrossCuttingConcerns.Interceptors;
using MemCaching.Abstract;
using MemCaching.Concrete;
using Newtonsoft.Json;

namespace MemCaching.CrossCuttingConcerns
{
    public class RemoveCacheAspect : MethodInterception
    {
        
        private readonly object _lock = new object();
        private ICacheService _cacheService;
        private bool _removeWithPattern;
        private string _key;

        public RemoveCacheAspect(string key = "", bool removeWithPattern = false)
        {
            _key = key;
            _removeWithPattern = removeWithPattern;
            _cacheService = new MemoryCacheManager();
        }

        public override void Intercept(IInvocation invocation)
        {
            lock (_lock)
            {

                if (_removeWithPattern)
                {
                    _cacheService.DeleteStartWithPattern(_key);
                }
                else
                {
                    var cacheKey = string.Concat(_key, ".", invocation.TargetType.FullName, ".", invocation.Method.Name, "(", JsonConvert.SerializeObject(invocation.Arguments), ")");
                    _cacheService.Delete(cacheKey);
                }
                    

                invocation.Proceed();

            }
        }
    }
}
