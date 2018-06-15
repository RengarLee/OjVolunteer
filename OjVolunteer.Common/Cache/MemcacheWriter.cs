using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Common.Cache
{
    public class MemcacheWriter : ICacheWriter
    {
        private MemcachedClient memcachedClient;

        public MemcacheWriter()
        {
            string strAppMemcachedServer = System.Configuration.ConfigurationManager.AppSettings["MemcachedServerList"];
            string[] servers = strAppMemcachedServer.Split(',');
            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 400;
            pool.MinConnections = 400;
            pool.MaxConnections = 1024;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            //客户端实例
            MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.EnableCompression = false;

            memcachedClient = mc;
        }

        public void AddCache(string key, object value, DateTime expTime)
        {
            memcachedClient.Add(key, value, expTime);
        }

        public void AddCache(string key, object value)
        {
            memcachedClient.Add(key, value);
        }

        public object GetCache(string key)
        {
            return memcachedClient.Get(key);
        }

        public T GetCache<T>(string key)
        {
            return (T)memcachedClient.Get(key);
        }

        public void SetCache(string key, object value, DateTime expTime)
        {
            memcachedClient.Set(key, value, expTime);
        }

        public void SetCache(string key, object value)
        {
            memcachedClient.Set(key, value);
        }
    }
}
