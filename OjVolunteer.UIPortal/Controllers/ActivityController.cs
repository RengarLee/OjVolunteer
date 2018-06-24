using OjVolunteer.BLL;
using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.UIPortal.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
        /// 志愿者用户查看活动详情
        /// </summary>
        /// <param name="id">活动Id</param>
        /// <returns></returns>
        [LoginCheckFilter(BoolCheckLogin = false)]
        public ActionResult Details(int Id)
        {
            var activity = ActivityService.GetEntities(u => u.ActivityID == Id).FirstOrDefault();
            if (activity == null)
            {
                return Redirect("/UserInfo/Index");
            }
            activity.ActivityClicks++;
            ActivityService.Update(activity);
            if (LoginUser != null)
            {
                ViewBag.UserId = LoginUser.UserInfoID;
            }
            else
            {
                ViewBag.UserId = -1;
            }
            ViewData.Model = activity;
            return View();
        }

        /// <summary>
        /// 团队查看活动详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult OrgSeeDetails(int id)
        {
            var activity = ActivityService.GetEntities(u => u.ActivityID == id).FirstOrDefault();
            ViewData.Model = activity;

            var major = MajorService.GetEntities(u => !activity.ActivityMajor.Contains(("," + u.MajorID.ToString() + ","))&&u.Status==delNormal).AsQueryable(); ;
            StringBuilder MajorStr = new StringBuilder();
            foreach (var i in major)
            {
                MajorStr.Append(i.MajorName+" ");
            }
            ViewBag.MajorStr = MajorStr;

            StringBuilder DepartmentStr = new StringBuilder();
            var department = DepartmentService.GetEntities(u => !activity.ActivityDepartment.Contains(("," + u.DepartmentID.ToString() + ",")) && u.Status == delNormal).AsQueryable();
            foreach (var i in department)
            {
                DepartmentStr.Append(i.DepartmentName + " ");
            }
            ViewBag.DepartmentStr = DepartmentStr;

            StringBuilder PoliticalStr = new StringBuilder();
            var political = PoliticalService.GetEntities(u=> !activity.ActivityPolitical.Contains(("," + u.PoliticalID.ToString() + ",")) && u.Status == delNormal).AsQueryable();
            foreach (var i in political)
            {
                PoliticalStr.Append(i.PoliticalName + " ");
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
            try
            {
                if (DateTime.Compare((DateTime)activity.ActivityStart, (DateTime)activity.ModfiedOn) == 0)
                {
                    return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {

            }
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

        #region 志愿者申请活动
        [ActionAuthentication(AbleUser = true)]
        public ActionResult UserCreate()
        {
            var allActivityType = ActivityTypeService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.ActivityTypeID = (from u in allActivityType select new SelectListItem() { Selected = false, Text = u.ActivityTypeName, Value = u.ActivityTypeID + "" }).ToList();
            ViewBag.MajorDict = MajorService.GetEntities(u => u.Status == delNormal).AsQueryable().ToDictionary(u => u.MajorID, u => u.MajorName);
            ViewBag.PoliticalDict = PoliticalService.GetEntities(u => u.Status == delNormal).AsQueryable().ToDictionary(u => u.PoliticalID, u => u.PoliticalName);
            ViewBag.DepartmentDict = DepartmentService.GetEntities(u => u.Status == delNormal).AsQueryable().ToDictionary(u => u.DepartmentID, u => u.DepartmentName);
            ViewBag.UserId = LoginUser.UserInfoID;
            return View();
        }
        #endregion

        #region 团队申请活动
        /// <summary>
        /// 团队进入活动申请界面
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
        [ActionAuthentication(AbleOrganize = true, AbleUser = true)]
        public JsonResult Create(Activity activity)
        {
            try
            {
                if (LoginOrganize != null)
                {
                    string[] EnrollTime = Request["EnrollTime"].Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                    string[] ActivtiyTime = Request["ActivityTime"].Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                    activity.ActivityEnrollStart = DateTime.Parse(EnrollTime[0]);
                    activity.ActivityEnrollEnd = DateTime.Parse(EnrollTime[1]);
                    activity.ActivityStart = DateTime.Parse(ActivtiyTime[0]);
                    activity.ActivityEnd = DateTime.Parse(ActivtiyTime[1]);
                }
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
                    activity.ActivityIcon = System.Configuration.ConfigurationManager.AppSettings["DefaultActivityIconPath"];
                }
                if (LoginOrganize != null)//组织注册
                {
                    activity.ActivityApplyOrganizeID = LoginOrganize.OrganizeInfoID;
                    //activity.Status = LoginOrganize.OrganizeInfoManageId == null ? delUndone : delAuditing;
                    activity.Status = activity.ActivityTypeID == 1 ? (LoginOrganize.OrganizeInfoManageId == null? delUndone:delAuditing) : delUndone;
                }
                else//
                {
                    activity.ActivityApplyUserInfoID = LoginUser.UserInfoID;
                    activity.ActivityApplyOrganizeID = LoginUser.OrganizeInfoID;
                    activity.Status = delAuditing;
                    activity.ActivityDepartment = ",";
                    activity.ActivityMajor = ",";
                    activity.ActivityPolitical = ",";
                }
                activity.ActivityClicks = 0;
                activity.CreateTime = DateTime.Now;
                activity.ModfiedOn = activity.CreateTime;

                if (ActivityService.AddActivity(activity))
                {
                    return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
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
                var data = new { total = total, rows = pageData.ToList() };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var pageData = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delAuditing, u => u.ActivityID, true).Select(u => new { u.ActivityID, u.ActivityName, u.ManagerUserInfo.OrganizeInfoID, u.ApplyUserInfo.UserInfoShowName, u.ApplyOrganizeInfo.OrganizeInfoShowName, u.ActivityPrediNum, u.ActivityType.ActivityTypeName, u.CreateTime, u.Status, }).AsQueryable();
                var data = new { total = total, rows = pageData.ToList() };
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
        [ActionAuthentication(AbleOrganize = true)]
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
                var pageData = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delDoneAuditing && o.ManagerUserInfo.OrganizeInfoID == LoginOrganize.OrganizeInfoID, u => u.ActivityID, true).Select(u => new { u.ManagerUserInfo.OrganizeInfoID, u.ActivityID, u.ActivityName, u.ApplyUserInfo.UserInfoShowName, u.ApplyOrganizeInfo.OrganizeInfoShowName, u.ActivityPrediNum, u.ActivityType.ActivityTypeName, u.ActivityStart, u.ActivityEnd, u.Status, u.ActivityManagerID, EnrollNum = u.UserEnroll.Count(), DetailNum = u.ActivityDetail.Count() }).AsQueryable();
                var data = new { total = total, rows = pageData.ToList() };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var pageData = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delDoneAuditing, u => u.ActivityID, true).Select(u => new { u.ManagerUserInfo.OrganizeInfoID, u.ActivityID, u.ActivityName, u.ApplyUserInfo.UserInfoShowName, u.ApplyOrganizeInfo.OrganizeInfoShowName, u.ActivityPrediNum, u.ActivityType.ActivityTypeName, u.ActivityStart, u.ActivityEnd, u.Status, u.ActivityManagerID, EnrollNum = u.UserEnroll.Count(), DetailNum = u.UserEnroll.Where(s => s.Status == delNormal).Count() }).AsQueryable();
                var data = new { total = total, rows = pageData.ToList() };
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 参考参与人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true)]
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
            var pageData = UserEnrollService.GetPageEntities(pageSize, pageIndex, out int total, u => u.ActivityID == id, u => u.ActivityID, true).Select(u => new { u.UserEnrollID, u.UserInfoID, u.UserInfo.UserInfoLoginId, u.UserInfo.UserInfoShowName, u.UserEnrollActivityStart, u.UserEnrollActivityEnd, u.ActivityTime }).AsQueryable();
            //var data = new { total = pageData.Count(), rows = pageData.ToList() };
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 通过
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true)]
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
        [ActionAuthentication(AbleOrganize = true)]

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
        [ActionAuthentication(AbleOrganize = true)]
        public ActionResult ActivityManager()
        {
            return View();
        }

        public JsonResult ActManData()
        {
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                var pageData = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, u => u.Status == delNormal && u.ActivityApplyOrganizeID == LoginOrganize.OrganizeInfoID, u => u.ActivityID, false).Select(u => new { u.ActivityID, u.ActivityName, u.ApplyUserInfo.UserInfoShowName, u.ApplyOrganizeInfo.OrganizeInfoShowName, u.ActivityPrediNum, u.ActivityType.ActivityTypeName, u.ActivityStart, u.ActivityEnd, u.Status, u.ActivityManagerID, EnrollNum = u.UserEnroll.Count(), DetailNum = u.ActivityDetail.Count() }).AsQueryable();
                var data = new { total = total, rows = pageData.ToList() };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var pageData = ActivityService.GetPageEntities(pageSize, pageIndex, out int total, u => u.Status == delNormal, u => u.ActivityID, false).Select(u => new { u.ActivityID, u.ActivityName, u.ApplyUserInfo.UserInfoShowName, u.ApplyOrganizeInfo.OrganizeInfoShowName, u.ActivityPrediNum, u.ActivityType.ActivityTypeName, u.ActivityStart, u.ActivityEnd, u.Status, u.ActivityManagerID, EnrollNum = u.UserEnroll.Count(), DetailNum = u.ActivityDetail.Count() }).AsQueryable();
                var data = new { total = total, rows = pageData.ToList() };
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region 志愿者浏览活动列表
        /// <summary>
        /// 志愿者用户进入活动列表
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(BoolCheckLogin = false)]
        public ActionResult List()
        {
            var activityType = ActivityTypeService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.ActivityTypeID = (from u in activityType select new SelectListItem { Value = u.ActivityTypeID + "", Text = u.ActivityTypeName }).ToList();

            var PageData = GetActivityData(5, 1);
            return View(PageData.ToList());
        }

        /// <summary>
        /// 志愿者用户浏览活动列表
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(BoolCheckLogin = false)]
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
                return Json(new { msg = "success", data = PageData.Select(u => new { u.ActivityIcon, u.ActivityName, u.ActivityEnrollEnd, u.ActivityStart, u.ActivityEnd, u.ActivityID, u.ActivityPrediNum, Count = u.UserEnroll.Count() }).ToList() }, JsonRequestBehavior.AllowGet);
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

            if (LoginUser != null)
            {
                DataPage = DataPage.Where(u => u.ActivityManagerID == LoginUser.UserInfoID||(!u.ActivityMajor.Contains("," + LoginUser.MajorID + ",") && !u.ActivityPolitical.Contains("," + LoginUser.PoliticalID + ",") && !u.ActivityDepartment.Contains("," + LoginUser.DepartmentID + ","))).AsQueryable();
            }
            return DataPage;
        }
        #endregion

        #region 我的活动

        /// <summary>
        /// 进入个人历史活动界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false,AbleUser =true)]
        public ActionResult ActivityOfUser()
        {
            return View();
        }
        /// <summary>
        /// 用户活动数据
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false,AbleUser =true)]
        public JsonResult ActivityOfUserData()
        {
            int pageSize = int.Parse(Request["pageSize"] ?? "5");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            int typeId = int.Parse(Request["typeId"] ?? "0");
            int UserInfoId = LoginUser.UserInfoID;
            if (typeId==0)//我参加的
            {
                var PageData = UserEnrollService.GetPageEntities(pageSize, pageIndex, out int total, u => u.UserInfoID == UserInfoId && u.Status == delNormal, u => u.CreateTime, false).Select(u=>new { u.Activity.ActivityName,u.Activity.ActivityIcon,u.UserEnrollActivityStart,u.UserEnrollActivityEnd,u.ActivityTime,u.ActivityID}).ToList();
                if (PageData.Count > 0)
                {
                    return Json(new { msg = "success", data=PageData }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
                }
            }
            else//我负责的
            {
                var PageData = UserEnrollService.GetPageEntities(pageSize, pageIndex, out int total, u => u.UserInfoID == UserInfoId && u.Status == delNormal&&u.Activity.ActivityManagerID == UserInfoId, u => u.CreateTime, false).Select(u => new { u.Activity.ActivityName, u.Activity.ActivityIcon, u.UserEnrollActivityStart, u.UserEnrollActivityEnd, u.ActivityTime, u.ActivityID }).ToList();
                if (PageData.Count > 0)
                {
                    return Json(new { msg = "success", data = PageData }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        #endregion

        #region 图片上传
        /// <summary>
        /// 活动正文图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadContentImage()
        {
            try
            {
                var file = Request.Files[0];
                String filePath = System.Configuration.ConfigurationManager.AppSettings["DefaultActivityImagesSavePath"];
                string dirPath = Request.MapPath(filePath);
                if (Common.FileUpload.FileHelper.ImageUpload(file, dirPath, filePath, out string fileName))
                {
                    return Json(new { code = 0, msg = "success", data = new { src = fileName } }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 1, msg = "fail" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                return Json(new { data = new { src = "" }, msg = "fail", code = 1 }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 活动头像上传
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadIcon()
        {
            try
            {
                //var file = Request.Files[0];
                //String filePath = System.Configuration.ConfigurationManager.AppSettings["DefaultActivityImagesSavePath"];
                //string path = filePath + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                //string dirPath = Request.MapPath(path);
                //if (!Directory.Exists(dirPath))
                //{
                //    Directory.CreateDirectory(dirPath);
                //}
                //string fileName = path + Guid.NewGuid().ToString().Substring(1, 10) + ".jpg";
                //System.Drawing.Image srcImg = Image.FromStream(file.InputStream, true, true);
                //Bitmap thumbImg = new Bitmap(srcImg.Width/3, srcImg.Height/3);
                //Graphics graphics = Graphics.FromImage(thumbImg);
                //thumbImg.SetResolution(srcImg.HorizontalResolution, srcImg.VerticalResolution); // 加上每 DPI（每英寸的点数），更精确。
                //graphics.DrawImage(srcImg, 0, 0, thumbImg.Width, thumbImg.Height);
                //thumbImg.Save(Server.MapPath(fileName));

                //graphics.Dispose();
                //thumbImg.Dispose();
                //srcImg.Dispose();

                var file = Request.Files[0];
                String filePath = System.Configuration.ConfigurationManager.AppSettings["DefaultActivityImagesSavePath"];
                string dirPath = Request.MapPath(filePath);
                if (Common.FileUpload.FileHelper.ImageUpload(file, dirPath,filePath, out string fileName))
                {
                    return Json(new { code = 0, msg = "success", data = new { src = fileName } }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 1, msg = "fail" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { code = 1, msg = "fail"}, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region 查看活动是否结束
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult IsEnd()
        {
            int Id = Convert.ToInt32(Request["Id"]);
            int Type = Convert.ToInt32(Request["TypeId"]);
            if (ActivityService.GetEntities(u => u.ActivityID == Id).FirstOrDefault().Status != delUndone)
            {
                return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
            }
            if (Type == 0)
            {
                return Json(new { msg = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = 1 }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 添加之前活动

        public ActionResult Test()
        {
            var allActivityType = ActivityTypeService.GetEntities(u => u.Status == delNormal).AsQueryable();
            var allOrganizeInfo = OrganizeInfoService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.ActivityTypeID = (from u in allActivityType select new SelectListItem() { Selected = false, Text = u.ActivityTypeName, Value = u.ActivityTypeID + "" }).ToList();
            ViewBag.ActivityApplyOrganizeID = (from u in allOrganizeInfo select new SelectListItem() { Selected = false, Text = u.OrganizeInfoShowName, Value = u.OrganizeInfoID + "" }).ToList();
            ViewBag.MajorDict = MajorService.GetEntities(u => u.Status == delNormal).AsQueryable().ToDictionary(u => u.MajorID, u => u.MajorName);
            ViewBag.PoliticalDict = PoliticalService.GetEntities(u => u.Status == delNormal).AsQueryable().ToDictionary(u => u.PoliticalID, u => u.PoliticalName);
            ViewBag.DepartmentDict = DepartmentService.GetEntities(u => u.Status == delNormal).AsQueryable().ToDictionary(u => u.DepartmentID, u => u.DepartmentName);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CreateBeforeActiivty(Activity activity)
        {
            string[] ActivtiyTime = Request["ActivityTime"].Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
            activity.ActivityStart = DateTime.Parse(ActivtiyTime[0]);
            activity.ActivityEnd = DateTime.Parse(ActivtiyTime[1]);
            activity.ActivityEnrollStart = (DateTime)activity.ActivityStart;
            activity.ActivityEnrollEnd = (DateTime)activity.ActivityStart;
            if (string.IsNullOrEmpty(activity.ActivityIcon))
            {
                activity.ActivityIcon = System.Configuration.ConfigurationManager.AppSettings["DefaultActivityIconPath"];
            }
            activity.Status = delDoneAuditing;
            activity.ActivityClicks = 0;
            activity.CreateTime = DateTime.Now;
            activity.ModfiedOn = activity.CreateTime;

            List<int> ids = new List<int>();
            String[] strIds = Request["UserIds"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var str in strIds)
            {
                ids.Add(Convert.ToInt32(str));
            }
            if (ActivityService.AddBeforeActivity(activity,ids))
            {
                return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}