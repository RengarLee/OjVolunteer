using OjVolunteer.BLL;
using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Enum;
using OjVolunteer.Model.Param;
using OjVolunteer.UIPortal.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class UserInfoController : BaseController
    {

        short delNormal = (short)DelFlagEnum.Normal;
        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        short delDeleted = (short)Model.Enum.DelFlagEnum.Deleted;
        public IUserInfoService UserInfoService { get; set; }
        public IOrganizeInfoService OrganizeInfoService { get; set; }
        public IUserDurationService UserDurationService { get; set; }
        public IPoliticalService PoliticalService { get; set; }
        public IMajorService MajorService { get; set; }
        public IDepartmentService DepartmentService { get; set; }
        public ITalksService TalksService { get; set; }
        public IUserBadgeService UserBadgeService { get; set; }

        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult Index()
        {
            return View();
        }

        #region Query
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public JsonResult SearchActivityPeople()
        {
            int key = int.Parse(Request["key"]);
            var user =UserInfoService.GetEntities(u => u.UserInfoID == key).FirstOrDefault();

            return Json(new { login=user.UserInfoLoginId, showname = user.UserInfoShowName }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 义工信息管理
        /// <summary>
        /// 进入义工信息管理界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult AllUserInfo()
        {
            return View();
        }

        /// <summary>
        /// 加载义工信息
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult GetAllUserInfo()
        {
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            UserQueryParam userQueryParam = new UserQueryParam();
            if (!string.IsNullOrEmpty(Request["filter"]))
            {
                userQueryParam = Newtonsoft.Json.JsonConvert.DeserializeObject<UserQueryParam>(Request["filter"]);
            }
            userQueryParam.PageSize = pageSize;
            userQueryParam.PageIndex = pageIndex;
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                userQueryParam.OrganizeInfoID = LoginOrganize.OrganizeInfoID;
                userQueryParam.isSuper = false;
            }
            else
            {
                userQueryParam.isSuper = true;
            }
            userQueryParam.Total = 0;

            var pageData = UserInfoService.LoadPageData(userQueryParam).Select(u => new
            {
                u.UserInfoID,
                u.UserInfoLoginId,
                u.UserInfoShowName,
                u.Department.DepartmentName,
                u.OrganizeInfoID,
                u.OrganizeInfo.OrganizeInfoShowName,
                u.UserInfoEmail,
                u.Political.PoliticalName,
                u.Major.MajorName,
                u.UserInfoTalkCount,
                u.UserInfoLastTime,
                u.UserInfoPhone,
                u.UserInfoStuId,
                u.UserDuration.UserDurationNormalTotal,
                u.UserDuration.UserDurationPartyTotal,
                u.UserDuration.UserDurationPropartyTotal,
                u.UserDuration.UserDurationTotal,
                u.Status,
            }).AsQueryable();
            var data = new { total = userQueryParam.Total, rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 义工政治面貌审核
        /// <summary>
        /// 组织进入义工政治面貌审核界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult UserOfAuditing()
        {
            return View();
        }
        /// <summary>
        /// 加载政治面貌变更审核信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult GetAllUserOfAuditing()
        {

            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = UserInfoService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delAuditing, u => u.UserInfoID, true)
                .Select(u => new {
                    u.UserDuration.UserDurationPropartyTime,
                    u.UserInfoID,
                    u.UserInfoShowName,
                    u.UserInfoLoginId,
                    u.OrganizeInfoID,
                    u.PoliticalID,
                    u.Political.PoliticalName,
                    u.UpdatePoliticalID,
                    u.OrganizeInfo.OrganizeInfoShowName,
                    u.UserInfoPhone,
                    UpdateName = u.UpdatePolitical.PoliticalName,
                    u.Status,
                    u.ModfiedOn
                }).AsQueryable();
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                pageData = pageData.Where(u => u.OrganizeInfoID == LoginOrganize.OrganizeInfoID).AsQueryable();
                total = pageData.Count();
            }
            var data = new { total = total, rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 组织创建用户
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult Create()
        {
            var allMajor = MajorService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.MajorID = (from u in allMajor select new SelectListItem() { Selected = false, Text = u.MajorName, Value = u.MajorID + "" }).ToList();
            var allPolitical = PoliticalService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.PoliticalID = (from u in allPolitical select new SelectListItem() { Text = u.PoliticalName, Value = u.PoliticalID + "" }).ToList();
            var allDepartment = DepartmentService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.DepartmentID = (from u in allDepartment select new SelectListItem() { Selected = false, Text = u.DepartmentName, Value = u.DepartmentID + "" }).ToList();
            var allOrganizeInfo = OrganizeInfoService.GetEntities(u => u.Status == delNormal && u.OrganizeInfoManageId != null).AsQueryable();
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                allOrganizeInfo = allOrganizeInfo.Where(u => u.OrganizeInfoID == LoginOrganize.OrganizeInfoID).AsQueryable();
            }
            ViewBag.OrganizeInfoID = (from u in allOrganizeInfo select new SelectListItem() { Selected = false, Text = u.OrganizeInfoShowName, Value = u.OrganizeInfoID + "" }).ToList();
            UserInfo userInfo = new UserInfo();
            userInfo.UserInfoIcon = userInfo.UserInfoIcon = System.Configuration.ConfigurationManager.AppSettings["DefaultIconPath"];
            ViewData.Model = userInfo;
            return View();
        }


        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public JsonResult Create(UserInfo userInfo)
        {
            String msg = String.Empty;
            if (ModelState.IsValid)
            {
                string pwd = "000000";
                userInfo.UserInfoPwd = Common.Encryption.MD5Helper.Get_MD5(pwd);
                userInfo.Status = delNormal;

                if (UserInfoService.AddUser(userInfo))
                {
                    msg = "success";
                }
                else
                {
                    msg = "fail";
                }
            }
            msg = "fail";
            return Json(new {msg},JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 用户获得用户信息
        /// <summary>
        /// 义工查看个人中心
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult Self()
        {
            ViewBag.Duration = UserDurationService.GetEntities(u => u.UserDurationID == LoginUser.UserInfoID).FirstOrDefault().UserDurationTotal;
            ViewData.Model = UserInfoService.GetEntities(u => u.UserInfoID == LoginUser.UserInfoID).FirstOrDefault();
            return View();
        }

        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult UserInfo(int Id)
        {
            bool isSelf = LoginUser.UserInfoID == Id ? true : false;
            UserDuration userDuration = UserDurationService.GetEntities(u => u.UserDurationID == Id).FirstOrDefault();
            if (userDuration != null)
            {
                ViewData["UserDuration"] = userDuration;
            }
            ViewBag.isSelf = isSelf;
            if (isSelf)
            {
                return View(LoginUser);
            }
            else
            {
                UserInfo user = UserInfoService.GetEntities(u => u.UserInfoID == Id && u.Status == delNormal).FirstOrDefault();
                if (user == null)
                {
                    return View("Shared/Error.cshtml");
                }
                return View(user);
            }
        }
        #endregion

        #region 修改信息

        /// <summary>
        /// 头像上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadIcon()
        {
            var file = Request.Files["file"];
            String filePath = System.Configuration.ConfigurationManager.AppSettings["DefaultIconSavePath"];
            string path = filePath + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            string dirPath = Request.MapPath(path);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileName = path + Guid.NewGuid().ToString().Substring(1, 5) + "-" + file.FileName;
            file.SaveAs(Request.MapPath(fileName));
            UserInfo userInfo = UserInfoService.GetEntities(u => u.UserInfoID == LoginUser.UserInfoID).FirstOrDefault();
            userInfo.UserInfoIcon = fileName;
            userInfo.ModfiedOn = DateTime.Now;

            if (UserInfoService.Update(userInfo))
            {
                UpSessionUserInfo(userInfo);
                return Json(new { src = fileName, msg = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 用户修改自身资料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult UserEditUser()
        {
            var allDepartment = DepartmentService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewData["DepartmentList"] = (from u in allDepartment
                                          select new SelectListItem() { Text = u.DepartmentName, Value = u.DepartmentID + "" }).ToList();

            var allMajor = MajorService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewData["MajorList"] = (from u in allMajor
                                     select new SelectListItem() { Text = u.MajorName, Value = u.MajorID + "" }).ToList();

            var allPolitical = PoliticalService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewData["UpdatePoliticalList"] = (from u in allPolitical
                                               select new SelectListItem() { Text = u.PoliticalName, Value = u.PoliticalID + "" }).ToList();
            var allOrganizeInfo = OrganizeInfoService.GetEntities(u => u.Status == delNormal && u.OrganizeInfoManageId != null).AsQueryable();
            ViewData["OrganizeinfoList"] = (from u in allOrganizeInfo
                                            select new SelectListItem() { Text = u.OrganizeInfoShowName, Value = u.OrganizeInfoID + "" }).ToList();

            return View(LoginUser);
        }

        /// <summary>
        /// 用户修改自身资料
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult UserEditUser(UserInfo userInfo)
        {
            String msg = "fail";
            if (ModelState.IsValid)
            {
                UserInfo temp = UserInfoService.GetEntities(u => LoginUser.UserInfoID == u.UserInfoID).FirstOrDefault();
                if (temp == null || temp.UserInfoID != userInfo.UserInfoID)
                {
                    return Content(msg);
                }
                if (temp.UpdatePoliticalID != userInfo.UpdatePoliticalID)
                {
                    temp.Status = delAuditing;
                }
                //赋值
                temp.UserInfoShowName = userInfo.UserInfoShowName;
                temp.UserInfoStuId = userInfo.UserInfoStuId;
                temp.UserInfoPhone = userInfo.UserInfoPhone;
                temp.UserInfoEmail = userInfo.UserInfoEmail;
                temp.MajorID = userInfo.MajorID;
                temp.DepartmentID = userInfo.DepartmentID;
                temp.OrganizeInfoID = userInfo.OrganizeInfoID;
                temp.UpdatePoliticalID = userInfo.UpdatePoliticalID;
                temp.ModfiedOn = DateTime.Now;

                if (UserInfoService.Update(temp))
                {
                    
                    UpSessionUserInfo(temp);
                    if (temp.Status == delAuditing)
                    {
                        return Content("auditing");
                    }
                    return Content("success");
                }
            }
            return Content(msg);
        }

        /// <summary>
        /// 组织修改义工详细信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult OrgEditUser(int id)
        {
            UserInfo user = UserInfoService.GetEntities(u => u.UserInfoID == id).FirstOrDefault();
            if (user == null)
                return Redirect("/Login/Index");
            var allMajor = MajorService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.MajorList = (from u in allMajor select new SelectListItem() { Selected = false, Text = u.MajorName, Value = u.MajorID + "" }).ToList();
            var allPolitical = PoliticalService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.PoliticalList = (from u in allPolitical select new SelectListItem() { Text = u.PoliticalName, Value = u.PoliticalID + "" }).ToList();
            var allDepartment = DepartmentService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.DepartmentList = (from u in allDepartment select new SelectListItem() { Selected = false, Text = u.DepartmentName, Value = u.DepartmentID + "" }).ToList();
            var allOrganizeInfo = OrganizeInfoService.GetEntities(u => u.Status == delNormal && u.OrganizeInfoManageId != null).AsQueryable();
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                allOrganizeInfo = allOrganizeInfo.Where(u => u.OrganizeInfoID == LoginOrganize.OrganizeInfoID).AsQueryable();
            }
            ViewBag.OrganizeinfoList = (from u in allOrganizeInfo select new SelectListItem() { Selected = false, Text = u.OrganizeInfoShowName, Value = u.OrganizeInfoID + "" }).ToList();
            if (LoginOrganize.OrganizeInfoID != user.OrganizeInfoID && LoginOrganize.OrganizeInfoManageId != null)
            {
                return Redirect("/Login/Index");
            }
            ViewBag.Status = user.Status;
            ViewData.Model = user;
            return View();
        }

        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult OrgEditUser(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                userInfo.ModfiedOn = DateTime.Now;
                if (userInfo.OrganizeInfoID != LoginOrganize.OrganizeInfoID && LoginOrganize.OrganizeInfoManageId != null)
                {
                    return Content("fail");
                }
                if (UserInfoService.Update(userInfo))
                {
                    return Content("success");
                }
            }
            return Content("fail");
        }
        
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult ResetPwd(int id)
        {

            UserInfo user = UserInfoService.GetEntities(u => u.UserInfoID == id).FirstOrDefault();
            //TODO:
            user.UserInfoPwd = Common.Encryption.MD5Helper.Get_MD5("000000");
            if (UserInfoService.Update(user))
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult UpdatePwd()
        {
            String msg = "fail";
            string oldPwd = Request["old"];
            string newPwd = Request["new"];

            if (LoginUser.UserInfoPwd.Equals(Common.Encryption.MD5Helper.Get_MD5(oldPwd)))
            {
                Regex regex = new Regex(@"^[A-Za-z0-9]{6,12}$");
                if (regex.IsMatch(newPwd))
                {
                    LoginUser.UserInfoPwd = Common.Encryption.MD5Helper.Get_MD5(newPwd);
                    LoginUser.ModfiedOn = DateTime.Now;
                    if (UserInfoService.Update(LoginUser))
                    {
                        msg = "success";
                    }
                }
            }
            return Json(new { msg }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 政治面貌审核
        /// <summary>
        /// 批量处理同意用户转变政治面貌
        /// </summary>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
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
            if (UserInfoService.ListUpdatePolical(idList))
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        /// <summary>
        /// 驳回义工更正政治面貌申请
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
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
            if (UserInfoService.NormalListByULS(idList))
            {
                return Content("success");
            }
            else
            {
                return Content("fail");

            }
        }
        #endregion
        
        #region 重置密码

        #endregion

        #region 导出Excel文件
        public FileResult ExportExcel()
        {
            return File(UserInfoService.ExportToExecl(true, 2), "application/vnd.ms-excel", DateTime.Now.ToString("yyyyMMdd") + ".xls");
        }
        #endregion

        #region 检查登录名是否存在
        public JsonResult CheckUserName(string username)
        {
            var reslut = UserInfoService.GetEntities(u => u.UserInfoLoginId.Equals(username)).AsQueryable().Count() == 0;
            return Json(reslut, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 退出操作
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult Exit()
        {
            Response.Cookies["userLoginId"].Value = String.Empty;
            return Redirect("/Login/");
        }
        #endregion

        #region 五小时义工提示
        public JsonResult FiftyHours()
        {
            String msg = "fail";
            UserBadge userBadge = UserBadgeService.GetEntities(u => u.UserInfoID == LoginUser.UserInfoID && u.BadgeID == 1).FirstOrDefault();
            if (userBadge != null && LoginUser.UserInfoLastTime < userBadge.CreateTime)
            {
                msg = "success";
            }
            LoginUser.UserInfoLastTime = DateTime.Now;
            UserInfoService.Update(LoginUser);
            return Json(new { msg }, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}