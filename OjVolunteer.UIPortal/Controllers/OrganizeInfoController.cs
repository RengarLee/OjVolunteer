using OjVolunteer.BLL;
using OjVolunteer.Common.Encryption;
using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Param;
using OjVolunteer.UIPortal.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{

    public class OrganizeInfoController : BaseController
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        public IOrganizeInfoService OrganizeInfoService { get; set; }
        public IUserDurationService UserDurationService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        public IMajorService MajorService { get; set; }
        public ITalksService TalksService { get; set; }

        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult Index()
        {
            return View(LoginOrganize);
        }

        public ActionResult Welcome()
        {
            return View();
        }

        #region  组织信息管理
        /// <summary>
        /// 进入组织信息管理界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false,Super =true)]
        public ActionResult AllOrganizeInfo()
        {
            return View();
        }

        /// <summary>
        /// 加载组织信息
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize =true,AbleUser =false,Super = true)]
        public ActionResult GetAllOrganizeInfo()
        {
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            OrganizeQueryParam qrganizeQueryParam = new OrganizeQueryParam();
            if (!string.IsNullOrEmpty(Request["filter"]))
            {
                qrganizeQueryParam = Newtonsoft.Json.JsonConvert.DeserializeObject<OrganizeQueryParam>(Request["filter"]);
            }
            qrganizeQueryParam.PageSize = pageSize;
            qrganizeQueryParam.PageIndex = pageIndex;
            qrganizeQueryParam.Total = 0;
            var pageData = OrganizeInfoService.LoadPageData(qrganizeQueryParam, LoginOrganize.OrganizeInfoID)
                            .Select(u => new
                            {
                                u.OrganizeInfoID,
                                u.OrganizeInfoLoginId,
                                u.OrganizeInfoShowName,
                                u.OrganizeInfoPeople,
                                u.OrganizeInfoEmail,
                                u.OrganizeInfoPhone,
                                u.OrganizeInfoLastTime,
                                u.CreateTime,
                                u.Status,
                                u.ActivityCount
                            }).AsQueryable();
            var data = new { total = qrganizeQueryParam.Total, rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加组织
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
        public ActionResult Create()
        {
            OrganizeInfo organizeInfo = new OrganizeInfo();
            organizeInfo.OrganizeInfoIcon = System.Configuration.ConfigurationManager.AppSettings["DefaultIconPath"];
            ViewData.Model = organizeInfo;
            return View();
        }

        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
        public JsonResult Create(OrganizeInfo organizeInfo)
        {
            string msg = "fail";
            Regex regex1 = new Regex(@"^[0-9]{6,12}$");
            if (!ValidateName(organizeInfo.OrganizeInfoLoginId))
            {
                return Json(new { msg = "exist" }, JsonRequestBehavior.AllowGet);
            }
            if (regex1.IsMatch(organizeInfo.OrganizeInfoLoginId))
            {
                return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                organizeInfo.OrganizeInfoManageId = LoginOrganize.OrganizeInfoID;
                if (OrganizeInfoService.AddOrg(organizeInfo))
                {
                    msg = "success";
                }

            }
            return Json(new { msg },JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ValidateName 验证用户名是否重复
        public Boolean ValidateName(string loginId)
        {
            bool flag = false;
            UserInfo userInfo = UserInfoService.GetEntities(u => u.UserInfoLoginId == loginId).FirstOrDefault();
            if (userInfo == null)
            {
                OrganizeInfo organizeInfo = OrganizeInfoService.GetEntities(u => u.OrganizeInfoLoginId == loginId).FirstOrDefault();
                if (organizeInfo == null)
                {
                    flag = true;
                }
            }
            return flag;
        }
        #endregion
        #region 组织账号申请审核
        /// <summary>
        /// 进入组织信息审核界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
        public ActionResult OrganizeOfAuditing()
        {
            return View();
        }

        /// <summary>
        /// 加载未审核的组织信息
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
        public ActionResult GetAllOrganizeOfAuditing()
        {

            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = OrganizeInfoService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delAuditing && o.OrganizeInfoManageId == LoginOrganize.OrganizeInfoID, u => u.OrganizeInfoID, true).Select(u => new { u.OrganizeInfoID, u.OrganizeInfoPeople, u.OrganizeInfoPhone, u.OrganizeInfoShowName, u.CreateTime, u.OrganizeInfoLoginId }).AsQueryable();
            var data = new { total = total, rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 批量删除申请的组织账号
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
            if (OrganizeInfoService.InvalidListByULS(idList))
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        /// <summary>
        /// 批量通过组织申请
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
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
            #region 批量处理
            if (OrganizeInfoService.NormalListByULS(idList))
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
            #endregion
        }

        #endregion

        #region 退出操作
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult Exit()
        {
            Response.Cookies["userLoginId"].Value = String.Empty;
            return Redirect("/Login/index");
        }
        #endregion

        #region 导出Excel文件
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
        public FileResult ExportExcel()
        { 
            return File(OrganizeInfoService.ExportToExecl(), "application/vnd.ms-excel", DateTime.Now.ToString("yyyyMMdd") + ".xls");
        }
        #endregion

        #region 检查登录名是否存在
        public JsonResult CheckUserName(string username)
        {
            var reslut = OrganizeInfoService.GetEntities(u => u.OrganizeInfoLoginId.Equals(username)).AsQueryable().Count() == 0;
            return Json(reslut, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 组织修改组织信息

        /// <summary>
        /// 查看组织详细页面,包括组织心得与组织活动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
        public ActionResult OrganizeDetail(int id)
        {
            ViewData.Model = OrganizeInfoService.GetEntities(o => o.OrganizeInfoID == id).FirstOrDefault();
            return View();
        }


        /// <summary>
        /// 打开编辑窗口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult Edit(int id)
        {
            //非最高等级组织用户欲编辑其他用户信息
            if (LoginOrganize.OrganizeInfoManageId != null && LoginOrganize.OrganizeInfoID != id)
            {
                return Redirect("/OrganizeInfo/Index");
            }
            OrganizeInfo organizeInfo = OrganizeInfoService.GetEntities(p => p.OrganizeInfoID == id && p.Status == delNormal).FirstOrDefault();
            if (organizeInfo == null)
            {
                return Redirect("/OrganizeInfo/Index");
            }
            return View(organizeInfo);
        }

        /// <summary>
        /// 提交编辑申请
        /// </summary>
        /// <param name="organizeInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult Edit(OrganizeInfo organizeInfo)
        {
            if (ModelState.IsValid)
            {
                organizeInfo.ModfiedOn = DateTime.Now;
                if (OrganizeInfoService.Update(organizeInfo))
                {
                    return Content("success");
                }
                else
                {
                    return Content("fail");
                }
            }
            return Content("fail");
        }

        /// <summary>
        /// 头像上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadIcon()
        {
            var file = Request.Files["file"];
            String filePath =  System.Configuration.ConfigurationManager.AppSettings["DefaultIconSavePath"];
            string path = filePath + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            string dirPath = Request.MapPath(path);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileName = path + Guid.NewGuid().ToString().Substring(1, 10)+".jpg";
           
            file.SaveAs(Request.MapPath(fileName));
            OrganizeInfo organizeInfo = OrganizeInfoService.GetEntities(u => u.OrganizeInfoID == LoginOrganize.OrganizeInfoID).FirstOrDefault();
            organizeInfo.OrganizeInfoIcon = fileName;
            organizeInfo.ModfiedOn = DateTime.Now;
            if (OrganizeInfoService.Update(organizeInfo))
            {
                LoginOrganize.OrganizeInfoIcon = fileName;
                return Json(new { src = fileName, msg = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
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

            if (LoginOrganize.OrganizeInfoPwd.Equals(MD5Helper.Get_MD5(oldPwd)))
            {
                Regex regex = new Regex(@"^[A-Za-z0-9]{6,12}$");
                if (regex.IsMatch(newPwd))
                {
                    LoginOrganize.OrganizeInfoPwd = MD5Helper.Get_MD5(newPwd);
                    LoginOrganize.ModfiedOn = DateTime.Now;
                    if (OrganizeInfoService.Update(LoginOrganize))
                    {
                        msg = "success";
                    }
                }
            }
            return Json(new { msg }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}