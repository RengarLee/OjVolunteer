using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OjVolunteer.Common.Cache
{
    public class HttpRuntimeCacheWriter : ICacheWriter
    {
        public void AddCache(string key, object value, DateTime expTime)
        {
            HttpRuntime.Cache.Insert(key, value, null, expTime, TimeSpan.Zero);
        }

        public void AddCache(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value);
        }

        public object GetCache(string key)
        {
            return HttpRuntime.Cache[key];
        }

        public T GetCache<T>(string key)
        {
            return (T)HttpRuntime.Cache[key];
        }

        public void SetCache(string key, object value, DateTime expTime)
        {
            HttpRuntime.Cache.Remove(key);
            AddCache(key, value, expTime);
        }

        public void SetCache(string key, object value)
        {
            HttpRuntime.Cache.Remove(key);
            AddCache(key, value);
        }
    }
}
