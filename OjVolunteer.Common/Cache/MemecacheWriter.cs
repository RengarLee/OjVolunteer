using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Common.Cache
{
    public class MemecacheWriter : ICacheWriter
    {
        public void AddCache(string key, object value, DateTime expTime)
        {
            throw new NotImplementedException();
        }

        public void AddCache(string key, object value)
        {
            throw new NotImplementedException();
        }

        public object GetCache(string key)
        {
            throw new NotImplementedException();
        }

        public T GetCache<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void SetCache(string key, object value, DateTime expTime)
        {
            throw new NotImplementedException();
        }

        public void SetCache(string key, object value)
        {
            throw new NotImplementedException();
        }
    }
}
