﻿using OjVolunteer.BLL;
using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class OrganizeInfoController : OrganizeBaseController
    {
        // GET: OrganizeInfo
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        public IOrganizeInfoService OrganizeInfoService { get; set; }
        public IUserDurationService UserDurationService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        public IMajorService MajorService { get; set; }
        public ITalksService TalksService { get; set; }
        //跳转后台页面
        public ActionResult Index()
        {
            return View(LoginUser);
        }

        #region  加载所有组织 
        /// <summary>
        /// 进入组织信息管理界面
        /// </summary>
        /// <returns></returns>
        public ActionResult AllOrganizeInfo()
        {
            return View(LoginUser);
        }

        /// <summary>
        /// 加载组织信息
        /// </summary>
        /// <returns></returns>
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
            var pageData = OrganizeInfoService.LoadPageData(qrganizeQueryParam, LoginUser.OrganizeInfoID)
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
        /// 查看组织详细页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OrganizeDetail(int id)
        {
            ViewData.Model = OrganizeInfoService.GetEntities(o => o.OrganizeInfoID == id).FirstOrDefault();
            return View() ;
        }

        /// <summary>
        /// 进入组织信息审核界面
        /// </summary>
        /// <returns></returns>
        public ActionResult OrganizeOfAuditing()
        {
            return View(LoginUser);
        }

        /// <summary>
        /// 加载未审核的组织信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllOrganizeOfAuditing()
        {
            var total = 0;
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = OrganizeInfoService.GetPageEntities(pageSize, pageIndex, out total, o => o.Status == delAuditing && o.OrganizeInfoManageId == LoginUser.OrganizeInfoID,u =>u.OrganizeInfoID ,true).Select(u=>new {u.OrganizeInfoID,u.OrganizeInfoPeople,u.OrganizeInfoPhone,u.OrganizeInfoShowName,u.CreateTime,u.OrganizeInfoLoginId}).ToList();
            var data = new { total = total, rows=pageData };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 批量通过组织申请
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult Agree(string ids)
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
            //批量处理
            #region 批量处理
            if (OrganizeInfoService.NormalListByULS(idList) > 0)
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
            #endregion
        }

        #endregion

        #region 加载组织自身所有用户
        /// <summary>
        /// 进入义工信息管理界面
        /// </summary>
        /// <returns></returns>
        public ActionResult AllUserInfo()
        {
            return View(LoginUser);
        }

        /// <summary>
        /// 加载义工信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllUserInfo()
        {
            var total = 0;
            var s = Request["limit"];
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
            userQueryParam.Total = 0;

            var pageData = UserInfoService.LoadPageData(userQueryParam).Select(u => new
            {
                u.UserInfoID,
                u.UserInfoLoginId,
                u.UserInfoShowName,
                u.Department.DepartmentName,
                u.OrganizeinfoID,
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
            if (LoginUser.OrganizeInfoManageId != null)
            {
                pageData = pageData.Where(u => u.OrganizeinfoID == LoginUser.OrganizeInfoID).AsQueryable();
            }
            var data = new { total = pageData.Count(), rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进入政治面貌变更审核界面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserOfAuditing()
        {
            return View(LoginUser);
        }

        /// <summary>
        /// 加载政治面貌变更审核信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllUserOfAuditing()
        {
            var total = 0;
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = UserInfoService.GetPageEntities(pageSize, pageIndex, out total, o => o.Status == delAuditing, u => u.UserInfoID, true)
                .Select(u => new {
                    u.UserInfoID,
                    u.UserInfoShowName,
                    u.UserInfoLoginId,
                    u.OrganizeinfoID,
                    u.PoliticalID,
                    u.Political.PoliticalName,
                    u.UpdatePoliticalID,
                    u.OrganizeInfo.OrganizeInfoShowName,
                    u.UserInfoPhone,UpdateName=u.UpdatePolitical.PoliticalName,
                    u.Status,
                    u.ModfiedOn});
            if (LoginUser.OrganizeInfoManageId != null)
            {
                pageData = pageData.Where(u => u.OrganizeinfoID == LoginUser.OrganizeInfoID);
                total = pageData.Count();
            }

            var data = new { total = total, rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 同意用户转变政治面貌
        /// </summary>
        public ActionResult AgreeUser(string ids)
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
            //批量处理
            #region 批量处理
            if (UserInfoService.ListUpdatePolical(idList) > 0)
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
            #endregion
        }

        /// <summary>
        /// 驳回用户转变政治面貌
        /// </summary>
        public ActionResult OpposeUser(string ids)
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
            //批量处理
            #region 批量处理
            if (UserInfoService.NormalListByULS(idList) > 0)
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
            #endregion 批量处理
        }

        /// <summary>
        /// 用户的详细信息界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserInfoDetail(int id)
        {
            UserInfo user = UserInfoService.GetEntities(u => u.UserInfoID == id).FirstOrDefault();
            
            return View(user);
        }
        #endregion

        #region Add
        public ActionResult Add()
        {
            //TODO:打开添加对话框
            return View();
        }

        [HttpPost]
        public ActionResult Add(OrganizeInfo organizeInfo)
        {
            //TODO:Test
            organizeInfo.CreateTime = DateTime.Now;
            organizeInfo.ModfiedOn = DateTime.Now;
            organizeInfo.Status = delNormal;
            return Content("ok");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            //TODO:加载编辑对话框
            OrganizeInfo organizeInfo = OrganizeInfoService.GetEntities(p => p.OrganizeInfoID == id && p.Status == delNormal).FirstOrDefault();
            return View(organizeInfo);
        }
        [HttpPost]
        public ActionResult Edit(OrganizeInfo organizeInfo)
        {
            organizeInfo.ModfiedOn = DateTime.Now;
            organizeInfo.OrganizeInfoPwd = LoginUser.OrganizeInfoPwd;
            if (OrganizeInfoService.Update(organizeInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 获得自身组织信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSelf()
        {
            OrganizeInfo organizeInfo = OrganizeInfoService.GetEntities(u => u.OrganizeInfoID == LoginUser.OrganizeInfoID).FirstOrDefault();
            return View(organizeInfo);
        }
        #endregion

        #region Delete
        public ActionResult Delete(string ids)
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
            //批量删除
            #region 逻辑删除
            if (OrganizeInfoService.DeleteListByLogical(idList) > 0)
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
            #endregion
        }
        #endregion

        #region 重置密码
        [HttpPost]
        public ActionResult ResetPwd(int id)
        {
            try
            {
                UserInfo user = UserInfoService.GetEntities(u => u.UserInfoID == id).FirstOrDefault();
                user.UserInfoPwd = Common.Encryption.MD5Helper.Get_MD5("000000");
                UserInfoService.Update(user);
                return Content("success");
            }
            catch (Exception e)
            {
                return Content("fail");
            }
        }
        #endregion

        #region 退出操作
        public ActionResult Exit()
        {
            LoginUser = null;

            return Redirect("/Login/index");
        }
        #endregion

        #region 加载所有未审核的心得
        public ActionResult TalksOfAuditing()
        {
            return View(LoginUser);
        }

        public ActionResult GetTalksOfAuditing()
        {
            var total = 0;
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = TalksService.GetEntities(t => t.Status == delAuditing).Select(t => new {t.TalkID,t.UserInfoID ,t.UserInfo.UserInfoShowName, t.OrganizeInfoID, t.OrganizeInfo.OrganizeInfoShowName, t.ModfiedOn, t.TalkContent, t.Status}).AsQueryable();
            if (LoginUser.OrganizeInfoManageId != null)
            {
                pageData = pageData.Where(t => t.OrganizeInfoID == LoginUser.OrganizeInfoID && t.UserInfoID == null).AsQueryable();
            }
            var data = new { total = pageData.Count(), rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}