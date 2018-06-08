using OjVolunteer.IBLL;
using OjVolunteer.UIPortal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class TopController : BaseController
    {
        IActivityDetailService ActivityDetailService { get; set; }

        [ActionAuthentication(AbleUser = true)]
        public ActionResult Index()
        {
            ViewData.Model = LoginUser;
            return View();
        }

        public JsonResult ActivityData()
        {
            int pageSize = int.Parse(Request["pageSize"] ?? "5");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            int OrgId = int.Parse(Request["OrgId"] ?? "-1");
            int TimeType = int.Parse(Request["TimeType"] ?? "1");
            var PageData = ActivityDetailService.GetTop(OrgId, TimeType, pageSize, pageIndex);
            if (PageData.Count() > 0)
            {
                return Json(new { msg = "success", data = PageData }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "fail" });
            }
        }

        public JsonResult GetRank()
        {
            int typeId = int.Parse(Request["typeId"] ?? "0");
            int OrgId = int.Parse(Request["OrgId"] ?? "-1");
            int TimeType = int.Parse(Request["TimeType"] ?? "1");
            if (typeId == 0)//活动时长
            {
                int Rank = ActivityDetailService.GetRank(LoginUser.UserInfoID,OrgId,TimeType, out decimal time);
                return Json(new { Rank, Num = time }, JsonRequestBehavior.AllowGet);
            }
            else//心得点赞
            {
                int Rank = ActivityDetailService.GetRank(LoginUser.UserInfoID, OrgId, TimeType, out decimal time);
                return Json(new { Rank, Num = time }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}