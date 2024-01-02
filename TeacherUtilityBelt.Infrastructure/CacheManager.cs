using System.Runtime.Caching;
using Microsoft.Extensions.Options;
using TeacherUtilityBelt.Core.Abstractions;
using TeacherUtilityBelt.Core.Domain;

namespace TeacherUtilityBelt.Infrastructure;

public class CacheManager : ICacheManager
{
    private MemoryCache _cache; 
    private AppSettings _appSettings;

    public CacheManager(IOptions<AppSettings> options)
    {
        _cache = MemoryCache.Default;
        _appSettings = options.Value;
    }


    public void Add(string key, object obj)
    {
        if (_cache.Contains(key))
        {
            return;
        }

        var policy = new CacheItemPolicy
        {
            AbsoluteExpiration =
                    DateTimeOffset.Now.AddHours(_appSettings.CacheTimeout)
        };

        _cache.Add(key, obj ,policy);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool Exists(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        return _cache.Contains(key);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public object Get(string key)
    {
        if (!Exists(key))
        {
            return null;
        }

        return _cache.Get(key);
    }
}
