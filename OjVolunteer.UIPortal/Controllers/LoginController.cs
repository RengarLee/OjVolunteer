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
            String pwd = Common.Encryption.MD5Helper.Get_MD5(Request["LoginPwd"]);
            short delDeleted = (short)DelFlagEnum.Deleted;
            var organizeInfo = OrganizeInfoService.GetEntities(o => o.OrganizeInfoLoginId == name && o.OrganizeInfoPwd == pwd && o.Status != delDeleted).FirstOrDefault();
            if (organizeInfo != null)
            {
                UserToCache(organizeInfo);
                return Content("OrganizeInfo");
            }
            var userInfo = UserInfoService.GetEntities(u => u.UserInfoLoginId == name && u.UserInfoPwd == pwd && u.Status != delDeleted).FirstOrDefault();
            if (userInfo != null)
            {
                UserToCache(userInfo);
                return Content("UserInfo");
            }
            return Content("Error");
        }
        #endregion

        #region UserToCache 将用户信息存储
        public void UserToCache(Object user)
        {
            String userLoginId = new Guid().ToString();
            Common.Cache.CacheHelper.AddCache(userLoginId, user);
            Response.Cookies["userLoginId"].Value = userLoginId;
        }
        #endregion
    }
}