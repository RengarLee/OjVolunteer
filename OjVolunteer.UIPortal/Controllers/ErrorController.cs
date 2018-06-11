using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QrCodeError(int Id,int aId)
        {
            ViewBag.IsSuccess = false;
            ViewBag.Home = "/UserInfo/Index";
            switch (Id)
            {
                case 1:ViewBag.Msg = "您未报名该活动，无法签到，请联系活动负责人。";break;
                case 2:ViewBag.Msg = "您尚未签到，无法签退，请联系活动负责人。"; break;
                case 3:ViewBag.Msg = "系统出现异常，请联系活动负责人。"; break;
                case 4:ViewBag.Msg = "签到成功"; ViewBag.IsSuccess = true; ViewBag.Act = "/Activity/Details/?Id=" + aId;break;
                default:break;
            }
            return View();
        }
    }
}