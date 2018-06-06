using OjVolunteer.IBLL;
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
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ActivityData()
        {
            int pageSize = int.Parse(Request["pageSize"] ?? "5");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            int OrgId = int.Parse(Request["OrgId"] ?? "-1");
            int TimeSpan = int.Parse(Request["TimeSpan"] ?? "-1");
            var PageData = ActivityDetailService.GetTop(OrgId, TimeSpan, pageSize, pageIndex);
            if (PageData.Count() > 0)
            {
                return Json(new { msg = "success", data = PageData }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "fail" });
            }
        }
    }
}