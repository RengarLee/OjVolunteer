using OjVolunteer.IBLL;
using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class ActivityTypeController : Controller
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IActivityTypeService ActivityTypeService { get; set; }
        // GET: ActivityType
        public ActionResult Index()
        {
            return View();
        }

        #region  加载所有政治面貌 
        public ActionResult GetAllActivityType()
        {
            //TODO:分页使用  BS Table
            return View();
        }
        #endregion

        #region Add
        public ActionResult Add()
        {
            //TODO:打开添加对话框
            return View();
        }

        [HttpPost]
        public ActionResult Add(ActivityType activityType)
        {
            //TODO:Test
            activityType.CreateTime = DateTime.Now;
            activityType.ModfiedOn = DateTime.Now;
            activityType.Status = delNormal;
            return Content("ok");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            //TODO:加载编辑对话框
            ActivityType activityType = ActivityTypeService.GetEntities(p => p.ActivityTypeID == id && p.Status == delNormal).FirstOrDefault();
            return View(activityType);
        }

        [HttpPost]
        public ActionResult Edit(ActivityType activityType)
        {
            //TODO:Test
            string result = String.Empty;
            if (ActivityTypeService.Update(activityType))
            {
                result = "ok";
            }
            else
            {
                result = "error";
            }
            return Content(result);
        }
        #endregion

        #region Delete
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("Please Select!");
            }
            string[] strIds = Request["ids"].Split(',');
            List<int> idList = new List<int>();
            foreach (var strId in strIds)
            {
                idList.Add(int.Parse(strId));
            }
            //批量删除
            #region 逻辑删除
            if (ActivityTypeService.DeleteListByLogical(idList) > 0)
            {
                return Content("error");
            }
            else
            {
                return Content("ok");
            }
            #endregion
        }
        #endregion
    }
}