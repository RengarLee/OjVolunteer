using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class UserInfoController : UserBaseController
    {
        short delNormal = (short)DelFlagEnum.Normal;
        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        public IUserInfoService UserInfoService { get; set; }
        public IOrganizeInfoService OrganizeInfoService { get; set; }
        public IUserDurationService UserDurationService { get; set; }
        public IPoliticalService PoliticalService { get; set; }
        public IMajorService MajorService { get; set; }
        public IDepartmentService DepartmentService { get; set; }
        // GET: UserInfo
        public ActionResult Index()
        {
            return View(LoginUser);
        }

        #region 获得用户信息
        /// <summary>
        /// 用户获得用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetUser(int Id)
        {

            bool isSelf = LoginUser.UserInfoID == Id ? true : false;
            //ViewData["UserDuration"] = "";
            UserDuration userDuration = UserDurationService.GetEntities(u => u.UserDurationID == Id).FirstOrDefault();
            if (userDuration != null)
            {
                ViewData["UserDuration"] = userDuration;
            }
            ViewBag.isSelf = isSelf;
            if(isSelf)
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
        /// 进入更多资料界面
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSelf()
        {
            var allDepartment = DepartmentService.GetEntities(u => u.Status == delNormal).ToList();
            ViewData["DepartmentID"] = (from u in allDepartment
                                          select new SelectListItem() { Selected = false, Text = u.DepartmentName, Value = u.DepartmentID + "" }).ToList();

            var allMajor = MajorService.GetEntities(u => u.Status == delNormal).ToList();
            ViewData["MajorID"] = (from u in allMajor
                                     select new SelectListItem() { Selected = false, Text = u.MajorName, Value = u.MajorID + "" }).ToList();

            var allPolitical = PoliticalService.GetEntities(u => u.Status == delNormal ).ToList();
            ViewData["UpdatePoliticalID"] = (from u in allPolitical
                                         select new SelectListItem() { Selected = false, Text = u.PoliticalName, Value = u.PoliticalID + "" }).ToList();
            var allOrganizeInfo = OrganizeInfoService.GetEntities(u => u.Status == delNormal && u.OrganizeInfoManageId != null).ToList();
            ViewData["OrganizeinfoID"] = (from u in allOrganizeInfo
                                            select new SelectListItem() { Selected = false, Text = u.OrganizeInfoShowName, Value = u.OrganizeInfoID + "" }).ToList();

            return View(LoginUser);
        }


        #endregion

        #region Add
        public ActionResult Add()
        {
            //TODO:打开添加对话框
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserInfo userInfo)
        {
            //TODO:Test
            userInfo.CreateTime = DateTime.Now;
            userInfo.ModfiedOn = DateTime.Now;
            userInfo.Status = delNormal;
            return Content("ok");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            //TODO:加载编辑对话框
            UserInfo userInfo = UserInfoService.GetEntities(p => p.UserInfoID == id && p.Status == delNormal).FirstOrDefault();
            return View(userInfo);
        }
        /// <summary>
        /// 用户信息修改
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            
            string result = String.Empty;

            userInfo.UserInfoPwd = LoginUser.UserInfoPwd;
            userInfo.UserInfoTalkCount = LoginUser.UserInfoTalkCount;
            userInfo.UserInfoIcon = LoginUser.UserInfoIcon;
            userInfo.UserInfoTalkCount = LoginUser.UserInfoTalkCount;
            userInfo.PoliticalID = LoginUser.PoliticalID;
            userInfo.Remark = LoginUser.Remark;
            userInfo.ModfiedOn = DateTime.Now;
            if (userInfo.Political == userInfo.UpdatePolitical)
            {
                userInfo.Status = (short)Model.Enum.DelFlagEnum.Auditing;
            }

            if (UserInfoService.Update(userInfo))
            {
                if (userInfo.Status == delAuditing)
                {
                    return Content("auditing");
                }
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
            
        }
        #endregion

        #region Delete
        public ActionResult Delete(string ids)
        {
            //TODO:                                                                                                                                                                                                                                         
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
            if (UserInfoService.DeleteListByLogical(idList) > 0)
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

        #region 心得发布
        public ActionResult WriteTalk()
        {   

            return View();
        }

        /// <summary>
        /// 心得图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadImage()
        {
            var file = Request.Files["file"];
            var talkId = Request["id"];
            string path = "/Content/Upload/TalkImage/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            string dirPath = Request.MapPath(path);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileName = path + Guid.NewGuid().ToString().Substring(1, 5) + "-" + file.FileName;
            file.SaveAs(Request.MapPath(fileName));
            return View();
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


    }
}