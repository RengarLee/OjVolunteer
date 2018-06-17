using OjVolunteer.UIPortal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    [LoginCheckFilter(BoolCheckLogin = false)]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QrCodeError()
        {
            int Id = Convert.ToInt32(Request["Id"] ?? "3");
            int aId = Convert.ToInt32(Request["aId"]);
            int type = Convert.ToInt32(Request["type"]);
            ViewBag.Title = (type == 0) ? "签到": "签退";
            ViewBag.IsSuccess = false;
            ViewBag.Home = "/UserInfo/Index";
            switch (Id)
            {
                case 1:ViewBag.Msg = "您未报名该活动，无法签到，请联系活动负责人。";break;
                case 2:ViewBag.Msg = "您尚未签到，无法签退，请联系活动负责人。"; break;
                case 3:ViewBag.Msg = "系统出现异常，请联系活动负责人。"; break;
                case 4: ViewBag.Msg =  (type == 0)? "签到成功": "签退成功";  ViewBag.IsSuccess = true; ViewBag.Act = "/Activity/Details/?Id=" + aId;break;
                default:break;
            }
            return View();
        }
    }
}