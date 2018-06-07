using OjVolunteer.BLL;
using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.UIPortal.Filters;
using System;
using System.Collections.Generic;
using System.IO;
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
        short delInvalid = (short)Model.Enum.DelFlagEnum.Invalid;
        short delUndone = (short)Model.Enum.DelFlagEnum.Undone;
        short delDoneAuditing = (short)Model.Enum.DelFlagEnum.DoneAuditing;
        public IActivityTypeService ActivityTypeService { get; set; }
        public IActivityService ActivityService { get; set; }
        public IOrganizeInfoService OrganizeInfoService { get; set; }
        public IMajorService MajorService { get; set; }
        public IPoliticalService PoliticalService { get; set; }
        public IDepartmentService DepartmentService { get; set; }
        public IUserEnrollService UserEnrollService { get; set; }
        public IActivityDetailService ActivityDetailService { get; set; }
        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        #region 查看活动详情
        /// <summary>
        /// 义工用户查看活动详情
        /// </summary>
        /// <param name="id">活动Id</param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult Details(int Id)
        {
            var activity = ActivityService.GetEntities(u=>u.ActivityID == Id).FirstOrDefault();
            if (activity == null)
            {
                return Redirect("/UserInfo/Index");
            }
            ViewBag.UserId = LoginUser.UserInfoID;
            ViewData.Model = activity;
            return View();
        }

        /// <summary>
        /// 组织查看活动详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult OrgSeeDetails(int id)
        {
            var activity = ActivityService.GetEntities(u => u.ActivityID == id).FirstOrDefault();
            ViewData.Model = activity;
            var MajorStr = String.Empty;
            String[] MajorIds = activity.ActivityMajor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var i in MajorIds)
            {
                MajorStr += MajorService.GetEntities(u => u.MajorID.ToString().Equals(i)).FirstOrDefault().MajorName;
            }
            ViewBag.MajorStr = MajorStr;

            var DepartmentStr = String.Empty;
            String[] DepartmentIds = activity.ActivityDepartment.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var i in DepartmentIds)
            {
                DepartmentStr += DepartmentService.GetEntities(u => u.DepartmentID.ToString().Equals(i)).FirstOrDefault().DepartmentName;
            }
            ViewBag.DepartmentStr = DepartmentStr;

            var PoliticalStr = String.Empty;
            String[] PoliticalIds = activity.ActivityPolitical.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var i in PoliticalIds)
            {
                PoliticalStr += PoliticalService.GetEntities(u => u.PoliticalID.ToString().Equals(i)).FirstOrDefault().PoliticalName+"  ";
            }
            ViewBag.PoliticalStr = PoliticalStr;
            return View();
        } 
        #endregion

        #region 活动开始结束
        /// <summary>
        /// 活动开始
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult Start()
        {
            int aId = Convert.ToInt32(Request["aId"]);
            Activity activity = ActivityService.GetEntities(u => u.ActivityID == aId && u.Status == delUndone).FirstOrDefault();
            if (activity.ActivityManagerID == LoginUser.UserInfoID)
            {
                activity.ActivityStart = DateTime.Now;
                activity.ModfiedOn = activity.ActivityStart;
                
                if (ActivityService.Update(activity))
                {
                    return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 活动结束
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult End()
        {
            int aId = Convert.ToInt32(Request["aId"]);
            Activity activity = ActivityService.GetEntities(u => u.ActivityID == aId && u.Status == delUndone).FirstOrDefault();
            if (activity.ActivityManagerID == LoginUser.UserInfoID)
            {
                activity.ActivityEnd = DateTime.Now;
                activity.ModfiedOn = activity.ActivityEnd;
                activity.Status = delDoneAuditing;
                if (ActivityService.Update(activity))
                {
                    return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 组织申请活动
        /// <summary>
        /// 组织进入活动申请界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true)]
        public ActionResult OrgCreate()
        {
            var allActivityType = ActivityTypeService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.ActivityTypeID = (from u in allActivityType select new SelectListItem() { Selected = false, Text = u.ActivityTypeName, Value = u.ActivityTypeID + "" }).ToList();
            ViewBag.MajorDict = MajorService.GetEntities(u => u.Status == delNormal).AsQueryable().ToDictionary(u => u.MajorID, u => u.MajorName);
            ViewBag.PoliticalDict = PoliticalService.GetEntities(u => u.Status == delNormal).AsQueryable().ToDictionary(u => u.PoliticalID, u => u.PoliticalName);
            ViewBag.DepartmentDict = DepartmentService.GetEntities(u => u.Status == delNormal).AsQueryable().ToDictionary(u => u.DepartmentID, u => u.DepartmentName);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionAuthentication(AbleOrganize = true)]
        public JsonResult Create(Activity activity)
        {
            try
            {
                string[] EnrollTime = Request["EnrollTime"].Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                string[] ActivtiyTime = Request["ActivityTime"].Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                activity.ActivityEnrollStart = DateTime.Parse(EnrollTime[0]);
                activity.ActivityEnrollEnd = DateTime.Parse(EnrollTime[1]);
                activity.ActivityStart = DateTime.Parse(ActivtiyTime[0]);
                activity.ActivityEnd = DateTime.Parse(ActivtiyTime[1]);
                if (activity.ActivityEnrollEnd > activity.ActivityStart)
                {
                    return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
                }
                if (UserInfoService.GetEntities(u => u.UserInfoID == activity.ActivityManagerID).FirstOrDefault() == null)
                {
                    return Json(new { msg = "noexist" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
            }

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(activity.ActivityIcon))
                {
                    activity.ActivityIcon = System.Configuration.ConfigurationManager.AppSettings["DefaultIconPath"];
                }
                activity.ActivityApplyOrganizeID = LoginOrganize.OrganizeInfoID;
                activity.ActivityClicks = 0;
                activity.CreateTime = DateTime.Now;
                activity.ModfiedOn = activity.CreateTime;
                activity.Status = LoginOrganize.OrganizeInfoManageId == null ? delUndone : delAuditing;

                if (ActivityService.Add(activity) != null)
                {
                    UserEnroll userEnroll = new UserEnroll()
                    {
                        ActivityID = activity.ActivityID,
                        UserInfoID = activity.ActivityManagerID,
                        UserEnrollStart = activity.ActivityEnrollStart,
                        ModfiedOn = DateTime.Now,
                        CreateTime = DateTime.Now,
                        Status = delInvalid,
                    };
                    if (UserEnrollService.Add(userEnroll) != null)
                    {
                        return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 活动申请审核

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

            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                var pageData = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delAuditing && o.ActivityApplyUserInfoID != null && o.ApplyUserInfo.OrganizeInfoID == LoginOrganize.OrganizeInfoID, u => u.ActivityID, true).Select(u => new { u.ActivityID, u.ActivityName, u.ManagerUserInfo.OrganizeInfoID, u.ApplyUserInfo.UserInfoShowName, u.ApplyOrganizeInfo.OrganizeInfoShowName, u.ActivityPrediNum, u.ActivityType.ActivityTypeName, u.CreateTime, u.Status, }).AsQueryable();
                var data = new { total = pageData.Count(), rows = pageData.ToList() };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var pageData = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delAuditing, u => u.ActivityID, true).Select(u => new { u.ActivityID, u.ActivityName, u.ManagerUserInfo.OrganizeInfoID, u.ApplyUserInfo.UserInfoShowName, u.ApplyOrganizeInfo.OrganizeInfoShowName, u.ActivityPrediNum, u.ActivityType.ActivityTypeName, u.CreateTime, u.Status, }).AsQueryable();
                var data = new { total = pageData.Count(), rows = pageData.ToList() };
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

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
            if (ActivityService.UpdateListStatus(idList, delUndone))
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

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

        #region 活动完成后审核
        /// <summary>
        /// 进入活动完成审核界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ActAccAuditing()
        {
            return View();
        }

        /// <summary>
        /// 加载需审核数据
        /// </summary>
        /// <returns></returns>
        public JsonResult ActAccAuditingData()
        {
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                var pageData = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delDoneAuditing && o.ManagerUserInfo.OrganizeInfoID == LoginOrganize.OrganizeInfoID, u => u.ActivityID, true).Select(u => new { u.ManagerUserInfo.OrganizeInfoID, u.ActivityID, u.ActivityName, u.ApplyUserInfo.UserInfoShowName, u.ApplyOrganizeInfo.OrganizeInfoShowName, u.ActivityPrediNum, u.ActivityType.ActivityTypeName, u.ActivityStart, u.ActivityEnd, u.Status, u.ActivityManagerID }).AsQueryable();
                var data = new { total = pageData.Count(), rows = pageData.ToList() };
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var pageData = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delDoneAuditing, u => u.ActivityID, true).Select(u => new { u.ManagerUserInfo.OrganizeInfoID, u.ActivityID, u.ActivityName, u.ApplyUserInfo.UserInfoShowName, u.ApplyOrganizeInfo.OrganizeInfoShowName, u.ActivityPrediNum, u.ActivityType.ActivityTypeName, u.ActivityStart, u.ActivityEnd, u.Status, u.ActivityManagerID }).AsQueryable();
                var data = new { total = pageData.Count(), rows = pageData.ToList() };
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 参考参与人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Participants(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// 参加该活动的人员数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ParticipantsData(int id)
        {
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = UserEnrollService.GetPageEntities(pageSize, pageIndex, out int total, u => u.ActivityID == id, u => u.ActivityID, true).Select(u => new { u.UserEnrollID, u.UserInfoID, u.UserInfo.UserInfoShowName, u.UserEnrollActivityStart, u.UserEnrollActivityEnd, u.ActivityTime }).AsQueryable();
            //var data = new { total = pageData.Count(), rows = pageData.ToList() };
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 通过
        /// </summary>
        /// <returns></returns>
        public JsonResult ActAccPass()
        {
            string msg = String.Empty;
            int aId = Convert.ToInt32(Request["aId"]);
            if (ActivityService.AddTime(aId))
                msg = "success";
            else
                msg = "fail";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 不通过
        /// </summary>
        /// <returns></returns>
        public JsonResult ActAccNotPass()
        {
            string msg = String.Empty;
            int aId = Convert.ToInt32(Request["aId"]);
            Activity activity = ActivityService.GetEntities(u => u.ActivityID == aId && u.Status == delDoneAuditing).FirstOrDefault();
            if (activity == null)
            {
                msg = "fail";
            }
            else
            {
                activity.ModfiedOn = DateTime.Now;
                activity.Status = delInvalid;
                if (ActivityService.Update(activity))
                {
                    msg = "success";
                }
                else
                {
                    msg = "fail";
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 活动信息管理
        public ActionResult ActivityManager()
        {
            return View();
        }

        public JsonResult ActManData()
        {
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, u=>u.Status==delNormal, u => u.ActivityID, true).Select(u => new { u.ActivityID, u.ActivityName, u.ApplyUserInfo.UserInfoShowName, u.ApplyOrganizeInfo.OrganizeInfoShowName, u.ActivityPrediNum, u.ActivityType.ActivityTypeName, u.ActivityStart, u.ActivityEnd, u.Status, u.ActivityManagerID }).AsQueryable();
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                pageData = pageData.Where(u => u.ActivityManagerID == LoginOrganize.OrganizeInfoID).AsQueryable();
            }
            var data = new { total = pageData.Count(), rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 义工浏览义工商场
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
            var DataPage = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, u => u.Status == delUndone, u => u.ActivityEnrollStart, false).AsQueryable();

            DataPage = DataPage.Where(u => !u.ActivityMajor.Contains("," + LoginUser.MajorID + ",") && !u.ActivityPolitical.Contains("," + LoginUser.PoliticalID + ",") && !u.ActivityDepartment.Contains("," + LoginUser.DepartmentID + ",")).AsQueryable();

            DataPage = DataPage.Where(u => u.ActivityEnrollStart < DateTime.Now).AsQueryable();
            return DataPage;
        }
        #endregion

        #region 历史活动界面

        /// <summary>
        /// 进入个人历史活动界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false,AbleUser =true)]
        public ActionResult TalksOfUser()
        {
            return View();
        }
        /// <summary>
        /// 用户活动数据
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false,AbleUser =true)]
        public JsonResult TalkOfUserData()
        {
            int pageSize = int.Parse(Request["pageSize"] ?? "5");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            int UserInfoId = Convert.ToInt32(Request["userInfoId"]);
            var PageData = ActivityDetailService.GetPageEntities(pageSize, pageIndex, out int total, u => u.UserInfoId== UserInfoId, u => u.CreateTime, false).AsQueryable();
            if (UserInfoId != LoginUser.UserInfoID)
            {
                PageData = PageData.Where(u => u.Activity.Status == delNormal).AsQueryable();
            }
            return Json(new { PageData }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult UploadContentImage()
        {
            try
            {
                var file = Request.Files[0];
                String filePath = System.Configuration.ConfigurationManager.AppSettings["DefaultActivityImagesSavePath"];
                string path = filePath + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                string dirPath = Request.MapPath(path);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string fileName = path + Guid.NewGuid().ToString().Substring(1, 10) + ".jpg";

                file.SaveAs(Request.MapPath(fileName));

                LoginOrganize.OrganizeInfoIcon = fileName;
                return Json(new { data=new { src = fileName }, msg = "success",code=0 }, JsonRequestBehavior.AllowGet);
            }
            catch {
                return Json(new { data = new { src = "" } , msg = "fail",code=1 }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult UploadIcon()
        {
            try
            {
                var file = Request.Files[0];
                String filePath = System.Configuration.ConfigurationManager.AppSettings["DefaultActivityImagesSavePath"];
                string path = filePath + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                string dirPath = Request.MapPath(path);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string fileName = path + Guid.NewGuid().ToString().Substring(1, 10) + ".jpg";

                file.SaveAs(Request.MapPath(fileName));
                
                return Json(new { src = fileName , msg = "success",  }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { msg = "fail",  }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}