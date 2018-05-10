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
    public class ActivityController : BaseController
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        short delDelete = (short)Model.Enum.DelFlagEnum.Deleted;
        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        public IActivityTypeService ActivityTypeService { get; set; }
        public IActivityService ActivityService { get; set; }
        public IOrganizeInfoService OrganizeInfoService { get; set; }
        public IMajorService MajorService { get; set; }
        public IPoliticalService PoliticalService { get; set; }
        public IDepartmentService DepartmentService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        #region Query

        public ActionResult ActivityOfUser(int id)
        {
            //.Select(u => new { u.ActivityID, u.ActivityContent, u.ActivityEnrollEnd, u.ActivityStart, u.ActivityEnd, u.ActivityIcon, u.ActivityAddress, u.ActivityManagerID, u.ManagerUserInfo.UserInfoShowName, u.ManagerUserInfo.UserInfoPhone, u.ActivityType.ActivityTypeName, u.ActivityName })
            var activity = ActivityService.GetEntities(u => u.Status==delNormal && u.ActivityID == id).FirstOrDefault();
            if (activity == null)
            {
                return Redirect("/UserInfo/Index");
            }
            //ViewBag.data = activity;
            ViewData.Model = activity;
            return View();
        }

        /// <summary>
        /// 义工用户进入活动列表
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult List()
        {
            var activityType = ActivityTypeService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.ActivityTypeID = (from u in activityType select new SelectListItem { Value = u.ActivityTypeID + "", Text = u.ActivityTypeName }).ToList();

            var PageData = GetActivityData(5, 1);
            return View(PageData.ToList());
        }

        /// <summary>
        /// 义工用户浏览活动列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetListData()
        {
            int pageSize = int.Parse(Request["pageSize"] ?? "5");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            var PageData = GetActivityData(pageSize, pageIndex);
            if (!String.IsNullOrEmpty(Request["typeId"]))
            {
                int typeId = int.Parse(Request["typeId"]);
                PageData = PageData.Where(u => u.ActivityTypeID == typeId).AsQueryable();
            }
            if (PageData.Count() > 0)
            {
                return Json(new { msg = "success", data = PageData.Select(u => new { u.ActivityIcon, u.ActivityName, u.ActivityEnrollEnd, u.ActivityStart, u.ActivityEnd, u.ActivityID }).ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "fail" });
            }
        }

        /// <summary>
        /// 加载列表数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IQueryable<Activity> GetActivityData(int pageSize, int pageIndex)
        {
            var DataPage = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, u => u.Status == delNormal, u => u.ActivityEnrollStart, false).AsQueryable();

            DataPage = DataPage.Where(u => !u.ActivityMajor.Contains("," + LoginUser.MajorID + ",") && !u.ActivityPolitical.Contains("," + LoginUser.PoliticalID + ",") && !u.ActivityDepartment.Contains("," + LoginUser.DepartmentID + ",")).AsQueryable();

            DataPage = DataPage.Where(u => u.ActivityEnrollStart < DateTime.Now).AsQueryable();
            return DataPage;
        }

        /// <summary>
        /// 活动申请审核
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult ActivityOfAuditing()
        {
            return View();
        }
        
        /// <summary>
        /// 活动申请审核数据
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult GetAllActivityOfAuditing()
        {
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delAuditing, u => u.ActivityID, true).Select(u => new { u.ActivityID, u.ActivityName, u.ApplyUserInfo.UserInfoShowName, u.ApplyOrganizeInfo.OrganizeInfoShowName, u.ActivityPrediNum,u.ActivityType.ActivityTypeName,u.CreateTime,u.Status ,u.ActivityManagerID}).AsQueryable();
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                pageData = pageData.Where(u => u.ActivityManagerID == LoginOrganize.OrganizeInfoID).AsQueryable();
            }
            var data = new { total = pageData.Count(), rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create
        /// <summary>
            /// 组织进入活动申请界面
            /// </summary>
            /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true)]
        public ActionResult OrgCreate()
        {
            var allActivityType = ActivityTypeService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.ActivityTypeID = (from u  in allActivityType select new SelectListItem() { Selected = false, Text = u.ActivityTypeName, Value = u.ActivityTypeID + "" }).ToList();
            ViewBag.MajorDict = MajorService.GetEntities(u => u.Status == delNormal).AsQueryable().ToDictionary(u => u.MajorID, u => u.MajorName);
            ViewBag.PoliticalDict = PoliticalService.GetEntities(u => u.Status == delNormal).AsQueryable().ToDictionary(u => u.PoliticalID, u => u.PoliticalName);
            ViewBag.DepartmentDict = DepartmentService.GetEntities(u => u.Status == delNormal).AsQueryable().ToDictionary(u => u.DepartmentID, u => u.DepartmentName); 
            return View();
        }

        [HttpPost]
        [ActionAuthentication(AbleOrganize = true)]
        public ActionResult Create(Activity activity)
        {
            if (ModelState.IsValid)
            {
                //添加活动条件
                activity.ActivityPolitical = Request["politicalIds"] ?? "";
                activity.ActivityMajor = Request["majorIds"] ?? "";
                activity.ActivityDepartment = Request["departmentIds"] ?? "";
                if (string.IsNullOrEmpty(activity.ActivityIcon))
                {
                    activity.ActivityIcon = System.Configuration.ConfigurationManager.AppSettings["DefaultIconPath"];
                }                
                activity.ActivityManagerID = OrganizeInfoService.GetEntities(u => u.OrganizeInfoManageId == null).FirstOrDefault().OrganizeInfoID;
                activity.ActivityApplyOrganizeID = LoginOrganize.OrganizeInfoID;
                activity.ActivityClicks = 0;
                activity.CreateTime = DateTime.Now;
                activity.ModfiedOn = activity.CreateTime;
                activity.Status = LoginOrganize.OrganizeInfoManageId == null ? delNormal : delDelete;
                if (ActivityService.Add(activity) != null)
                {
                    return Content("success");
                }
            }
            return Content("fail");
        }
        #endregion

        #region Edit

        /// <summary>
        /// 批量同意活动申请
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true)]
        public ActionResult EditOfList(string ids)
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
            if (ActivityService.NormalListByULS(idList))
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// 批量删除申请的活动
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
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
            if (ActivityService.InvalidListByULS(idList))
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