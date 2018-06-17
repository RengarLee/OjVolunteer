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
            int TimeType = int.Parse(Request["TimeType"] ?? "1");
            var PageData = ActivityDetailService.GetTop(OrgId, TimeType, pageSize, pageIndex,out int total);
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
            var PageData = ActivityDetailService.GetTop(OrgId, TimeType, pageSize, pageIndex, out int total);
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