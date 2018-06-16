using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Common.WxHelper
{
    /// <summary>
    /// 微信配置信息
    /// </summary>
    public class WxConfig
    {
        /// <summary>
        /// 返回APPID
        /// </summary>
        /// <returns></returns>
        public static String GetAPPID()
        {
            string appId = Cache.CacheHelper.GetCache("WxAppId") as String;
            if (appId == null)
            {
                appId = "";
                Cache.CacheHelper.SetCache("WxAppId", appId);
            }
            return appId;
        }



        /// <summary>
        /// 生成随机时间
        /// </summary>
        /// <returns></returns>
        public static String GenerateTimeStamp()
        {
            return (DateTime.Now.Millisecond / 1000).ToString();
        }
    }
}
