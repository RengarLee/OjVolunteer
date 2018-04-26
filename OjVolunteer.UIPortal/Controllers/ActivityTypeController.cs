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
    public class ActivityTypeController : BaseController
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IActivityTypeService ActivityTypeService { get; set; }

        [ActionAuthentication(AbleOrganize = true, Super = true)]
        public ActionResult Index()
        {
            return View(LoginOrganize);
        }

        #region Query

        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, Super = true)]
        public ActionResult GetAllActivityType()
        {
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = ActivityTypeService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delNormal, u => u.ActivityTypeID, true).Select(u => new { u.ActivityTypeName, u.ModfiedOn, u.ActivityTypeID }).ToList();
            var data = new { total = total, rows = pageData };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create

        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, Super = true)]
        public ActionResult Create(ActivityType activityType)
        {
            if (ModelState.IsValid)
            {
                if (ActivityTypeService.GetEntities(m => m.ActivityTypeName.Equals(activityType.ActivityTypeName)&&m.Status == delNormal).Count() > 0)
                {
                    return Content("exist");
                }
                activityType.CreateTime = DateTime.Now;
                activityType.ModfiedOn = DateTime.Now;
                activityType.Status = delNormal;
                if (ActivityTypeService.Add(activityType) != null)
                {
                    return Content("success");
                }
            }
            return Content("fail");

        }
        #endregion

        #region Edit

        /// <summary>
        /// 行编辑活动类型名称
        /// </summary>
        /// <param name="activityType"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, Super = true)]
        public ActionResult Edit(ActivityType activityType)
        {
            if (ModelState.IsValid)
            {
                string result = String.Empty;
                activityType.ModfiedOn = DateTime.Now;
                ActivityType temp = ActivityTypeService.GetEntities(d => d.ActivityTypeName.Equals(activityType.ActivityTypeName) && d.Status == delNormal).FirstOrDefault();
                if (temp != null)
                {
                    result = ("exist");
                }
                else
                {
                    if (ActivityTypeService.Update(activityType))
                    {
                        result = "success";
                    }
                    else
                    {
                        result = "fail";
                    }
                }
                return Json(new { result = result }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = "fail" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete
        [ActionAuthentication(AbleOrganize = true, Super = true)]
        public ActionResult DeleteOfList(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("null");
            }
            string[] strIds = Request["ids"].Split(',');
            List<int> idList = new List<int>();
            foreach (var strId in strIds)
            {
                idList.Add(int.Parse(strId));
            }
            if (ActivityTypeService.DeleteListByLogical(idList) > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }
        #endregion
    }
}