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
                activity.ActivityPolitical = Request["politicalIds"];
                activity.ActivityMajor = Request["majorIds"];
                activity.ActivityDepartment = Request["departmentIds"];
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

    }
}