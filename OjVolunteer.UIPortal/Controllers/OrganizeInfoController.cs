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
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{

    public class OrganizeInfoController : BaseController
    {
        #region 存入缓存
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        #endregion
        public IOrganizeInfoService OrganizeInfoService { get; set; }
        public IUserDurationService UserDurationService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        public IMajorService MajorService { get; set; }
        public ITalksService TalksService { get; set; }

        /// <summary>
        /// 跳转后台页面
        /// </summary>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult Index()
        {
            return View(LoginOrganize);
        }

        #region  Query
        /// <summary>
        /// 进入组织信息管理界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false,Super =true)]
        public ActionResult AllOrganizeInfo()
        {
            return View(LoginOrganize);
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
        /// 进入组织信息审核界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false,Super =true)]
        public ActionResult OrganizeOfAuditing()
        {
            return View(LoginOrganize);
        }

        /// <summary>
        /// 加载未审核的组织信息
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false,Super =true)]
        public ActionResult GetAllOrganizeOfAuditing()
        {
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = OrganizeInfoService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delAuditing && o.OrganizeInfoManageId == LoginOrganize.OrganizeInfoID,u =>u.OrganizeInfoID ,true).Select(u=>new {u.OrganizeInfoID,u.OrganizeInfoPeople,u.OrganizeInfoPhone,u.OrganizeInfoShowName,u.CreateTime,u.OrganizeInfoLoginId}).AsQueryable();
            var data = new { total = total, rows=pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
        public ActionResult Create(OrganizeInfo organizeInfo)
        {
            if (ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(organizeInfo.OrganizeInfoIcon))
                {
                     organizeInfo.OrganizeInfoIcon = System.Configuration.ConfigurationManager.AppSettings["DefaultIconPath"];
                }
                //organizeInfo.OrganizeInfoPwd = MD5Helper.Get_MD5(organizeInfo.OrganizeInfoPwd);
                organizeInfo.OrganizeInfoManageId = LoginOrganize.OrganizeInfoID;
                organizeInfo.ActivityCount = 0;
                organizeInfo.CreateTime = DateTime.Now;
                organizeInfo.ModfiedOn = organizeInfo.CreateTime;
                organizeInfo.OrganizeInfoLastTime = organizeInfo.CreateTime;
                organizeInfo.Status = delNormal;
                OrganizeInfoService.Add(organizeInfo);
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }
        #endregion

        #region Edit
        /// <summary>
        /// 打开编辑窗口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult Edit(int id)
        {
            //非最高等级组织用户欲编辑其他用户信息
            if(LoginOrganize.OrganizeInfoManageId!=null&&LoginOrganize.OrganizeInfoID != id)
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
                organizeInfo.OrganizeInfoPwd = LoginOrganize.OrganizeInfoPwd;
                if (OrganizeInfoService.Update(organizeInfo))
                {
                    return Content("ok");
                }
                else
                {
                    return Content("error");
                }
            }
            return Content("error");
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
            if (OrganizeInfoService.NormalListByULS(idList) > 0)
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

        #region Delete
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
            #region 逻辑删除
            if (OrganizeInfoService.DeleteListByLogical(idList) > 0)
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

    }
}