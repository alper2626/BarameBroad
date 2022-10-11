using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BaseEntities.Response;
using Castle.DynamicProxy;
using CrossCuttingConcerns.Interceptors;
using MemCaching.Abstract;
using MemCaching.Concrete;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace MemCaching.CrossCuttingConcerns
{
    public class AddCacheAspect : MethodInterception
    {

        //    private readonly object _lock = new object();
        //    private string _removeKey;
        //    private ICacheService _cacheService;
        //    private int _slidingMin;

        //    public AddCacheAspect(string removeKey, int slidingMin = 1)
        //    {
        //        _cacheService = new MemoryCacheManager();
        //        _slidingMin = slidingMin;
        //        _removeKey = removeKey;
        //    }

        //    public override void Intercept(IInvocation invocation)
        //    {
        //        lock (_lock)
        //        {
        //            var genericNames = invocation.GenericArguments != null ? invocation.GenericArguments.ToList() : new
        //                List<Type>();
        //            var genericStr = genericNames.Select(q => q.Name);
        //            var cacheKey = string.Concat(_removeKey, ".", invocation.TargetType.FullName, ".",
        //                invocation.Method.Name, string.Join('.', genericStr), "(",
        //                JsonConvert.SerializeObject(invocation.Arguments), ")");



        //            var obj = _cacheService.Get(cacheKey);

        //            var foundFlag = false;
        //            if (obj != null)
        //            {

        //                if (genericNames.Count == 0)
        //                {
        //                    foundFlag = true;
        //                    invocation.ReturnValue = obj;
        //                }
        //                else if (genericNames.Any(q => q.Name == obj.Data.GetType().Name))
        //                {
        //                    foundFlag = true;
        //                    invocation.ReturnValue = obj;

        //                }
        //                else if (obj.Data is IList)
        //                {
        //                    var name = ((IList)obj.Data).GetType().GetGenericArguments()[0].Name;
        //                    if (genericNames.Any(w => w.Name == name))
        //                    {
        //                        foundFlag = true;
        //                        invocation.ReturnValue = obj;

        //                    }
        //                }
        //            }
        //            if(!foundFlag)
        //            {
        //                invocation.Proceed();
        //                var returnValue = invocation.ReturnValue;
        //                var response = returnValue as DataResponse;
        //                if (response != null && response.Success)
        //                {
        //                    _cacheService.AddOrUpdate(cacheKey, invocation.ReturnValue, new MemoryCacheEntryOptions
        //                    {
        //                        Priority = CacheItemPriority.Low,
        //                        AbsoluteExpiration = DateTime.Now.AddHours(1),
        //                        SlidingExpiration = TimeSpan.FromMinutes(_slidingMin)
        //                    });

        //                }


        //            }
        //        }
        //    }
    }
}

