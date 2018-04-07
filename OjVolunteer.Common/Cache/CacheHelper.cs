using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Common.Cache
{
    public class CacheHelper
    {
        //SpringNet 在创建实例对象的时候才会从xml读取信息进行属性注入，但属性是static类 如果不进行实例创建 该属性将没有复制
        public static ICacheWriter CacheWriter { get; set; }

        static CacheHelper()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            ctx.GetObject("CacheHelper");
        }

        #region 添加缓存
        public static void AddCache(string key, object value, DateTime expDate)
        {
            CacheWriter.AddCache(key, value, expDate);
        }

        public static void AddCache(string key, object value)
        {
            CacheWriter.AddCache(key, value);
        }

        #endregion

        #region 设置缓存
        public static void SetCache(string key, object value, DateTime expDate)
        {
            CacheWriter.SetCache(key, value, expDate);
        }

        public static void SetCache(string key, object value)
        {
            CacheWriter.SetCache(key, value);
        }
        #endregion

        #region 获得缓冲
        public static object GetCache(string key)
        {
            return CacheWriter.GetCache(key);
        }

        public static T GetCache<T>(string key)
        {
            return CacheWriter.GetCache<T>(key);
        }
        #endregion

    }
}
