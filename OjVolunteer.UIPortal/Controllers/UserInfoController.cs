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
    public class UserInfoController : UserBaseController
    {
        short delNormal = (short)DelFlagEnum.Normal;
        public IUserInfoService UserInfoService { get; set; }
        public IUserDurationService UserDurationService { get; set; }
        // GET: UserInfo
        public ActionResult Index()
        {
            UserInfo user = LoginUser;
            return View(user);
        }

        #region 获得用户信息
        public ActionResult GetUser(int Id)
        {

            bool isSelf = LoginUser.UserInfoID == Id ? true : false;
            //ViewData["UserDuration"] = "";
            UserDuration userDuration = UserDurationService.GetEntities(u => u.UserInfoID == Id).FirstOrDefault();
            if (userDuration != null)
            {
                ViewData["UserDuration"] = userDuration;
            }
            ViewBag.isSelf = isSelf;
            if(isSelf)
            {
                return View(LoginUser);
            }
            else
            {
                UserInfo user = UserInfoService.GetEntities(u => u.UserInfoID == Id && u.Status == delNormal).FirstOrDefault();
                if (user == null)
                {
                    return View("Shared/Error.cshtml");
                }
                return View(user);
            }
        }
        #endregion

    }
}