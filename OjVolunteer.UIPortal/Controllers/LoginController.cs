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

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProcessLogin()
        {
            String name = Request["LoginCode"];
            String pwd = Request["LoginPwd"];
            short delNormal = (short)DelFlagEnum.Normal;
            var organizeinfo = OrganizeInfoService.GetEntities(o => o.OrganizeInfLoginId == name && o.OrganizeInfoPwd == pwd && o.Status == delNormal).FirstOrDefault();
            if (organizeinfo != null)
            {
                return Content("OrganizeInfo");
            }
            var userinfo = UserInfoService.GetEntities(u => u.UserInfoLoginId == name && u.UserInfoPwd == pwd && u.Status == delNormal).FirstOrDefault();
            if (userinfo != null)
            {
                return Content("UserInfo");
            }
            return Content("Error");
        }
    }
}