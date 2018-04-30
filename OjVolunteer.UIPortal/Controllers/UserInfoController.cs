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



        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult Index()
        {
            return View();
        }

        #region Query
        /// <summary>
        /// 用户通过用户获得用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult UserInfoSimple(int Id)
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
            else {
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

        /// <summary>
        /// 组织进入义工政治面貌审核界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult UserOfAuditing()
        {
            return View(LoginOrganize);
        }
        /// <summary>
        /// 加载政治面貌变更审核信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult GetAllUserOfAuditing()
        {
            
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = UserInfoService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delAuditing, u => u.UserInfoID, true)
                .Select(u => new {
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

        #region Create
        public ActionResult Create()
        {
            var allMajor = MajorService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.Major = (from u in allMajor select new SelectListItem() { Selected = false, Text = u.MajorName, Value = u.MajorID + "" }).ToList();
            var allPolitical = PoliticalService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.PoliticalID = (from u in allPolitical select new SelectListItem() { Selected = false, Text = u.PoliticalName, Value = u.PoliticalID + "" }).ToList();
            var allDepartment = DepartmentService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.DepartmentID = (from u in allDepartment select new SelectListItem() { Selected = false, Text = u.DepartmentName, Value = u.DepartmentID + "" }).ToList();
            var allOrganizeInfo = OrganizeInfoService.GetEntities(u => u.Status == delNormal && u.OrganizeInfoManageId != null).AsQueryable();
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                allOrganizeInfo = allOrganizeInfo.Where(u => u.OrganizeInfoID == LoginOrganize.OrganizeInfoID).AsQueryable() ;
            }
            ViewBag.OrganizeInfoID = (from u in allOrganizeInfo  select new SelectListItem() { Selected = false, Text = u.OrganizeInfoShowName, Value = u.OrganizeInfoID + "" }).ToList();
            return View();
        }


        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(userInfo.UserInfoIcon))
                {
                    userInfo.UserInfoIcon = System.Configuration.ConfigurationManager.AppSettings["DefaultIconPath"];
                }
                userInfo.UserInfoTalkCount = 0;
                userInfo.CreateTime = DateTime.Now;
                userInfo.ModfiedOn = userInfo.CreateTime;
                userInfo.UserInfoLastTime = userInfo.CreateTime;
                userInfo.Status = delAuditing;
                UserInfoService.Add(userInfo);
                return Content("success");
            }
            return Content("fail");
        }
        #endregion

        #region Edit

        /// <summary>
        /// 用户修改自身资料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult UserEditUser(int id)
        {
            if(LoginUser.UserInfoID != id)
            {
                return Redirect("/UserInfo/Index");
            }
            var allDepartment = DepartmentService.GetEntities(u => u.Status == delNormal).ToList();
            ViewData["DepartmentID"] = (from u in allDepartment
                                        select new SelectListItem() { Selected = false, Text = u.DepartmentName, Value = u.DepartmentID + "" }).ToList();

            var allMajor = MajorService.GetEntities(u => u.Status == delNormal).ToList();
            ViewData["MajorID"] = (from u in allMajor
                                   select new SelectListItem() { Selected = false, Text = u.MajorName, Value = u.MajorID + "" }).ToList();

            var allPolitical = PoliticalService.GetEntities(u => u.Status == delNormal).ToList();
            ViewData["UpdatePoliticalID"] = (from u in allPolitical
                                             select new SelectListItem() { Selected = false, Text = u.PoliticalName, Value = u.PoliticalID + "" }).ToList();
            var allOrganizeInfo = OrganizeInfoService.GetEntities(u => u.Status == delNormal && u.OrganizeInfoManageId != null).ToList();
            ViewData["OrganizeinfoID"] = (from u in allOrganizeInfo
                                          select new SelectListItem() { Selected = false, Text = u.OrganizeInfoShowName, Value = u.OrganizeInfoID + "" }).ToList();

            return View(LoginUser);
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
            if(user == null)
                return Redirect("/OrganizeInfo/Index");
            var allMajor = MajorService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.MajorID = (from u in allMajor select new SelectListItem() { Selected = false, Text = u.MajorName, Value = u.MajorID + "" }).ToList();
            var allPolitical = PoliticalService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.PoliticalID = (from u in allPolitical select new SelectListItem() { Selected = false, Text = u.PoliticalName, Value = u.PoliticalID + "" }).ToList();
            var allDepartment = DepartmentService.GetEntities(u => u.Status == delNormal).AsQueryable();
            ViewBag.DepartmentID = (from u in allDepartment select new SelectListItem() { Selected = false, Text = u.DepartmentName, Value = u.DepartmentID + "" }).ToList();
            var allOrganizeInfo = OrganizeInfoService.GetEntities(u => u.Status == delNormal && u.OrganizeInfoManageId != null).AsQueryable();
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                allOrganizeInfo = allOrganizeInfo.Where(u => u.OrganizeInfoID == LoginOrganize.OrganizeInfoID).AsQueryable();
            }
            ViewBag.OrganizeInfoID = (from u in allOrganizeInfo select new SelectListItem() { Selected = false, Text = u.OrganizeInfoShowName, Value = u.OrganizeInfoID + "" }).ToList();
            if (LoginOrganize.OrganizeInfoID!=user.OrganizeInfoID&& LoginOrganize.OrganizeInfoManageId!=null)
            {
                return Redirect("/OrganizeInfo/Index");
            }
            return View(user);
        }

        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult OrgEditUser(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                userInfo.ModfiedOn = DateTime.Now;
                if (UserInfoService.Update(userInfo))
                {
                    return Content("success");
                }
            }
            return Content("fail");
        }

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
        /// 修改信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            UserInfo temp = UserInfoService.GetEntities(u => u.UserInfoID == userInfo.UserInfoID).FirstOrDefault();
            if (temp == null)
            {
                return Content("fail");
            }
            if (LoginUser != null)
            {
                if (temp.UpdatePoliticalID != userInfo.UpdatePoliticalID)
                {
                    temp.UpdatePoliticalID = userInfo.UpdatePoliticalID;
                    temp.Status = (short)Model.Enum.DelFlagEnum.Auditing;
                }
            }
            else
            {
                if (userInfo.OrganizeInfoID != LoginOrganize.OrganizeInfoID && LoginOrganize.OrganizeInfoManageId != null)
                {
                    return Content("fail");
                }
            }

            temp.UserInfoShowName = userInfo.UserInfoShowName;
            temp.UserInfoStuId = userInfo.UserInfoStuId;
            temp.UserInfoPhone = userInfo.UserInfoPhone;
            temp.UserInfoEmail = userInfo.UserInfoEmail;
            temp.MajorID = userInfo.MajorID;
            temp.OrganizeInfoID = userInfo.OrganizeInfoID;
            temp.ModfiedOn = DateTime.Now;
            if (UserInfoService.Update(temp))
            {
                if (temp.Status == delAuditing)
                {
                    return Content("auditing");
                }
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
        /// 驳回义工更正政治面貌申请
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult DeleteOfList(string ids)
        {
            //TODO:                                                                                                                                                                                                                                         
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
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult ResetPwd(int id)
        {

                UserInfo user = UserInfoService.GetEntities(u => u.UserInfoID == id).FirstOrDefault();
            //TODO:
                user.UserInfoPwd = Common.Encryption.MD5Helper.Get_MD5("000000");
                if(UserInfoService.Update(user))
                {
                    return Content("success");
                }
                else
                {
                    return Content("fail");
                }
        }
        #endregion

        #region 头像更换
        public ActionResult UploadIcon()
        {
            var file = Request.Files["file"];
            string path = "/Content/Upload/Icon/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            string dirPath = Request.MapPath(path);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileName = path + Guid.NewGuid().ToString().Substring(1, 5) + "-" + file.FileName;
            file.SaveAs(Request.MapPath(fileName));
            UserInfo userInfo = UserInfoService.GetEntities(u => u.UserInfoID == LoginUser.UserInfoID).FirstOrDefault();
            userInfo.UserInfoIcon = fileName;

            if (UserInfoService.Update(userInfo))
            {
                LoginUser.UserInfoIcon = fileName;
                return Json(new { src = fileName, msg = "ok" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "error" }, JsonRequestBehavior.AllowGet);
            }

        }
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

    }
}