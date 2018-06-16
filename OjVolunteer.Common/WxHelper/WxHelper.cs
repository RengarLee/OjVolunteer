using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace OjVolunteer.Common.WxHelper
{
    public class WxHelper
    {
        /// <summary>

        ///   返回使用微信JS-SDK接口的配置

        //    appId: @ViewBag.wx_appid, // 必填，企业号的唯一标识，此处填写企业号corpid

        //    timestamp: @ViewBag.wx_timestamp, // 必填，生成签名的时间戳

        //    nonceStr: @ViewBag.wx_noncestr, // 必填，生成签名的随机串

        //    signature: @ViewBag.wx_signature,// 必填，签名，见附录1

        /// </summary>

        /// <returns></returns>

        //public WxPayData GetJSSDKConfig()

        //{

        //    string appid = WxConfig.GetAPPID();

        //    string secret = WxPayConfig.APPSECRET;

        //    string timestamp = WxConfig.GenerateTimeStamp();

        //    string noncestr = WxPayApi.GenerateNonceStr();

        //    string signature = "";


        //    //签名算法  https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141115

        //    //1. 获取AccessToken（有效期7200秒，开发者必须在自己的服务全局缓存access_token）

        //    string url1 = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={secret}";

        //    string result = HttpService.Get(url1);

        //    JsonData jd = JsonMapper.ToObject(result);

        //    string access_token = (string)jd["access_token"];

        //    //2. 用第一步拿到的access_token 采用http GET方式请求获得jsapi_ticket（有效期7200秒，开发者必须在自己的服务全局缓存jsapi_ticket）

        //    string url2 = $"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={access_token}&type=jsapi";

        //    string result2 = HttpService.Get(url2);

        //    JsonData jd2 = JsonMapper.ToObject(result2);
        //    string ticket = (string)jd2["ticket"];


        //    //3. 开始签名
        //    string now_url = HttpContext.Current.Request.Url.AbsoluteUri;
        //    string no_jiami = $"jsapi_ticket={ticket}&noncestr={noncestr}&timestamp={timestamp}&url={now_url}";

        //    //SHA1加密
        //    signature = FormsAuthentication.HashPasswordForStoringInConfigFile(no_jiami, "SHA1");


        //    WxPayData data = new WxPayData();
        //    data.SetValue("appId", appid);
        //    data.SetValue("timestamp", timestamp);
        //    data.SetValue("nonceStr", noncestr);
        //    data.SetValue("signature", signature);
        //    return data;
        //}
    }
}
