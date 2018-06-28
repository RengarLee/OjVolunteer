using OjVolunteer.IBLL;
using OjVolunteer.Model;
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
        IOrganizeInfoService OrganizeInfoService { get; set; }
        ITalksService TalksService { get; set; }
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        /// <summary>
        /// 义工查看排行榜界面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(BoolCheckLogin =false)]
        public ActionResult Index()
        {
            if (LoginUser != null)
            {
                ViewData.Model = LoginUser;
                ViewBag.isLogin = true;
            }
            else
            {
                ViewBag.isLogin = false ;
            }
            return View();
        }

        /// <summary>
        /// 团队查看排行榜界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true)]
        public ActionResult OrgIndex()
        {
            if (LoginOrganize.OrganizeInfoManageId == null)
            {
                ViewBag.OrganizeInfoList = OrganizeInfoService.GetEntities(u => u.Status == delNormal && u.OrganizeInfoManageId != null).ToList();
            }
            else
            {
                ViewBag.OrganizeInfoList = new List<OrganizeInfo>() { LoginOrganize };
            }
            return View();
        }

        public JsonResult OrgActivityData()
        {
            int pageSize = int.Parse(Request["pageSize"] ?? "5");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            int OrgId = int.Parse(Request["OrgId"] ?? "-1");
            DateTime Start = DateTime.Parse(Request["Start"]);
            DateTime End = DateTime.Parse(Request["End"]).AddDays(1);
            var PageData = ActivityDetailService.GetTopCache(OrgId, Start, End, pageSize, pageIndex, out int total);
            return Json(new { total, rows = PageData.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OrgTalkData()
        {
            int pageSize = int.Parse(Request["pageSize"] ?? "5");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            int OrgId = int.Parse(Request["OrgId"] ?? "-1");
            int TimeType = int.Parse(Request["TimeType"] ?? "1");
            var PageData = TalksService.GetTop(OrgId, TimeType, pageSize, pageIndex, out int total);
            return Json(new { total, rows = PageData.ToList() }, JsonRequestBehavior.AllowGet);
        }

        [LoginCheckFilter(BoolCheckLogin = false)]
        public JsonResult ActivityData()
        {
            int pageSize = int.Parse(Request["pageSize"] ?? "5");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            int OrgId = int.Parse(Request["OrgId"] ?? "-1");
            int TimeType = int.Parse(Request["TimeType"] ?? "1");
            DateTime dt = DateTime.Now;
            DateTime Start;
            if (TimeType == 1)
            {
                Start = DateTime.Parse(dt.ToString("yyyy-MM"));
            }
            else
            {
                Start = new DateTime(dt.Year, 1, 1);
            }
            DateTime End = DateTime.Now;

            var PageData = ActivityDetailService.GetTopCache(OrgId, Start, End, pageSize, pageIndex, out int total);
            if (PageData.Count() > 0)
            {
                return Json(new { msg = "success", data = PageData }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "fail",total });
            }
        }

        public JsonResult GetRank()
        {
            int typeId = int.Parse(Request["typeId"] ?? "0");
            int OrgId = int.Parse(Request["OrgId"] ?? "-1");
            int TimeType = int.Parse(Request["TimeType"] ?? "1");
            DateTime dt = DateTime.Now;
            if (TimeType == 1)// 月排行
            {
                DateTime Start = DateTime.Parse(dt.ToString("yyyy-MM"));
                DateTime End = dt;
                int Rank = ActivityDetailService.GetRankCache(LoginUser.UserInfoID, OrgId, Start,End, out decimal time);
                return Json(new { Rank, Num = time }, JsonRequestBehavior.AllowGet);
            }
            else// 年排行
            {
                DateTime Start = new DateTime(dt.Year, 1, 1);
                DateTime End = dt;
                int Rank = ActivityDetailService.GetRankCache(LoginUser.UserInfoID, OrgId, Start,End, out decimal time);
                return Json(new { Rank, Num = time }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}