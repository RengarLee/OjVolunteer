using OjVolunteer.Common.Encryption;
using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class LoginController : Controller
    {
        public IOrganizeInfoService OrganizeInfoService { get; set; }
        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        #region ProcessLogin 验证登录
        public ActionResult ProcessLogin()
        {
            String name = Request["LoginCode"];
            String pwd = MD5Helper.Get_MD5(Request["LoginPwd"]);
            short delDeleted = (short)DelFlagEnum.Deleted;
            short delInvalid = (short)DelFlagEnum.Invalid;
            var organizeInfo = OrganizeInfoService.GetEntities(o => o.OrganizeInfoLoginId == name && o.OrganizeInfoPwd == pwd && o.Status != delDeleted && o.Status != delInvalid).FirstOrDefault();
            if (organizeInfo != null)
            {
                organizeInfo.OrganizeInfoLastTime = DateTime.Now;
                OrganizeInfoService.Update(organizeInfo);
                UserToCache(organizeInfo);
                return Content("OrganizeInfo");
            }
            var userInfo = UserInfoService.GetEntities(u => u.UserInfoLoginId == name && u.UserInfoPwd == pwd && u.Status != delDeleted&&u.Status != delInvalid).FirstOrDefault();
            if (userInfo != null)
            {
                //userInfo.UserInfoLastTime = DateTime.Now;
                //UserInfoService.Update(userInfo);
                UserToCache(userInfo);
                return Content("userinfo");
            }
            return Content("fail");
        }
        #endregion

        #region UserToCache 将登录的用户信息存入缓存
        public void UserToCache(Object info)
        {
            String userLoginId = Guid.NewGuid().ToString();
            Common.Cache.CacheHelper.AddCache(userLoginId,info);
            Response.Cookies["userLoginId"].Value = userLoginId;
        }
        #endregion
    }
}