using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Common.Cache
{
    public interface ICacheWriter
    {
        void AddCache(string key, object value, DateTime expTime);
        void AddCache(string key, object value);
        void SetCache(string key, object value, DateTime expTime);
        void SetCache(string key, object value);
        object GetCache(string key);
        T GetCache<T>(string key);
    }
}
