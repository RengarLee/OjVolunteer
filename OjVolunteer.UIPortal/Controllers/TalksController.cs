using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Param;
using OjVolunteer.Model.ViewModel;
using OjVolunteer.UIPortal.Filters;
using OjVolunteer.UIPortal.Models;
using Spring.Web.Support;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class TalksController : BaseController
    {

        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        short delDeleted = (short)Model.Enum.DelFlagEnum.Deleted;
        short delInvalid = (short)Model.Enum.DelFlagEnum.Invalid;
        public ITalksService TalksService { get; set; }
        public IFavorsService FavorsService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region  Query

        /// <summary>
        /// 通过路径查看图片
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTalksImage()
        {
            string path = Request["ImagePath"];
            List<String> imageList = new List<string>();
            if (path != null)
            {
                var files = Directory.GetFiles(Request.MapPath(path));
                foreach (var file in files)
                {
                    int i = file.LastIndexOf("\\");
                    imageList.Add(file.Substring(i + 1));
                }
            }
            return Json(new { total = imageList.Count(), data = imageList }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 用户心得列表
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 获得心得数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetListData()
        {
            int pageSize = int.Parse(Request["pageSize"] ?? "5");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            var PageData = TalksService.GetPageEntities(pageSize, pageIndex, out int total, u => u.Status == delNormal, u => u.CreateTime, false).AsQueryable();

            if (PageData.Count() > 0)
            {
                List<TalkViewModel> list = new List<TalkViewModel>();
                foreach (var data in PageData)
                {
                    TalkViewModel talk = new TalkViewModel();
                    talk.TalkID = data.TalkID;

                    talk.TalkFavorsNum = (int)data.TalkFavorsNum;
                    talk.CreateTime = (DateTime)data.CreateTime;
                    if (data.UserInfo != null)
                    {
                        talk.ShowName = data.UserInfo.UserInfoShowName;
                        talk.Icon = data.UserInfo.UserInfoIcon;
                    }
                    else
                    {
                        talk.ShowName = data.OrganizeInfo.OrganizeInfoShowName;
                        talk.Icon = data.OrganizeInfo.OrganizeInfoIcon;
                    }
                    talk.TalkContent = data.TalkContent;

                    if (data.TalkImagePath != null)
                    {
                        var files = Directory.GetFiles(Request.MapPath(data.TalkImagePath));
                        List<String> pathlist = new List<String>();
                        foreach (var file in files)
                        {
                            int i = file.LastIndexOf("\\");
                            pathlist.Add(data.TalkImagePath + file.Substring(i + 1));
                        }
                        talk.ImagePath = pathlist;
                    }
                    talk.UserInfoId = (int)data.UserInfoID;
                    talk.Favors = FavorsService.GetEntities(u => u.TalkID == talk.TalkID && u.UserInfoID == LoginUser.UserInfoID).Count()>0;
                    list.Add(talk);
                }
                return Json(new { msg = "success", data = list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "fail" });
            }
        }

        #region 历史心得界面

        /// <summary>
        /// 进入个人历史心得界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult TalksOfUser(int Id)
        {
            ViewBag.UserId = Id;
            return View();
        }
        /// <summary>
        /// 用户心得数据
        /// </summary>
        /// <returns></returns>
        public JsonResult TalkOfUserData()
        {
            int pageSize = int.Parse(Request["pageSize"] ?? "5");
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            int UserInfoId = Convert.ToInt32(Request["userInfoId"]);
            if (UserInfoId != LoginUser.UserInfoID)
            {
                var PageData = TalksService.GetPageEntities(pageSize, pageIndex, out int total, u => u.UserInfoID == UserInfoId&&u.Status==delNormal, u => u.CreateTime, false).AsQueryable();
                var data = LoadImagePath(PageData);
                if (data.Count > 0)
                {
                    return Json(new { msg = "success", data }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var PageData = TalksService.GetPageEntities(pageSize, pageIndex, out int total, u => u.UserInfoID == UserInfoId, u => u.CreateTime, false).AsQueryable();
                var data = LoadImagePath(PageData);
                if (data.Count > 0)
                {
                    return Json(new { msg = "success", data }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
                }
            }
            
        }
        #endregion

        #region 加载心得图片 
        private List<TalkView> LoadImagePath(IQueryable<Talks> Data)
        {
            List<TalkView> list = new List<TalkView>();
            if (Data.Count() > 0)
            {
                foreach (var data in Data)
                {
                    TalkView talk = new TalkView
                    {
                        TalkID = data.TalkID,
                        TalkFavorsNum = (int)data.TalkFavorsNum,
                        CreateTime = (DateTime)data.CreateTime,
                        Status = data.Status
                    };
                    if (data.UserInfo != null)
                    {
                        talk.ShowName = data.UserInfo.UserInfoShowName;
                        talk.Icon = data.UserInfo.UserInfoIcon;
                    }
                    else
                    {
                        talk.ShowName = data.OrganizeInfo.OrganizeInfoShowName;
                        talk.Icon = data.OrganizeInfo.OrganizeInfoIcon;
                    }
                    talk.TalkContent = data.TalkContent;

                    if (data.TalkImagePath != null)
                    {
                        var files = Directory.GetFiles(Request.MapPath(data.TalkImagePath));
                        List<String> pathlist = new List<String>();
                        foreach (var file in files)
                        {
                            int i = file.LastIndexOf("\\");
                            pathlist.Add(data.TalkImagePath + file.Substring(i + 1));
                        }
                        talk.ImagePath = pathlist;
                    }
                    list.Add(talk);
                }

            }
            return list;
        }
        #endregion

        #endregion

        #region 组织心得管理
        public ActionResult AllTalks()
        {
            return View();
        }

        public ActionResult GetAllTalks()
        {
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            TalkQueryParam talkQueryParam = new TalkQueryParam();
            if (!string.IsNullOrEmpty(Request["filter"]))
            {
                talkQueryParam = Newtonsoft.Json.JsonConvert.DeserializeObject<TalkQueryParam>(Request["filter"]);
            }
            talkQueryParam.PageSize = pageSize;
            talkQueryParam.PageIndex = pageIndex;
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                talkQueryParam.OrganizeInfoID = LoginOrganize.OrganizeInfoID;
                talkQueryParam.isSuper = false;
            }
            else
            {
                talkQueryParam.isSuper = true;
            }
            talkQueryParam.Total = 0;

            var pageData = TalksService.LoadPageData(talkQueryParam).Select(u => new
            {
                u.TalkID,
                u.UserInfo.UserInfoShowName,
                u.OrganizeInfo.OrganizeInfoShowName,
                u.TalkFavorsNum,
                u.CreateTime,
                u.Status
            }).AsQueryable();
            var data = new { total = talkQueryParam.Total, rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 组织根据用户ID查看该用户发表的发表的心得列表
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = true)]
        public ActionResult GetTalkByUserId()
        {
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            if (String.IsNullOrEmpty(Request["userId"]))
            {
                return Json(new { total = 0, rows = "" }, JsonRequestBehavior.AllowGet);
            }
            int userId = Convert.ToInt32(Request["userId"]);
            var pageData = TalksService.GetPageEntities(pageSize, pageIndex, out int total, u => u.UserInfoID == userId, u => u.CreateTime, false).Select(n => new { n.TalkID, n.UserInfo.UserInfoShowName, n.TalkImagePath, n.TalkFavorsNum, n.TalkContent, n.Status, n.CreateTime, n.ModfiedOn }).ToList();

            var data = new { total = total, rows = pageData };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 组织根据组织ID获得组织其下发表的心得
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = true)]
        public ActionResult GetTalkByOrgId()
        {
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            if (String.IsNullOrEmpty(Request["orgId"]))
            {
                return Json(new { total = 0, rows = "" }, JsonRequestBehavior.AllowGet);
            }
            int orgId = Convert.ToInt32(Request["orgId"]);
            var pageData = TalksService.GetPageEntities(pageSize, pageIndex, out int total, u => u.OrganizeInfoID == orgId, u => u.CreateTime, false).Select(n => new { n.TalkID, n.UserInfo.UserInfoShowName, n.TalkImagePath, n.TalkFavorsNum, n.TalkContent, n.Status, n.CreateTime, n.ModfiedOn }).ToList();

            var data = new { total = total, rows = pageData };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 组织根据心得ID查看心得详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult TalkDetail(int id)
        {
            Talks talks = TalksService.GetEntities(u => u.TalkID == id).FirstOrDefault();
            List<String> imageList = new List<string>();
            if (talks.TalkImagePath != null)
            {
                var files = Directory.GetFiles(Request.MapPath(talks.TalkImagePath));
                foreach (var file in files)
                {
                    int i = file.LastIndexOf("\\");
                    imageList.Add(file.Substring(i + 1));
                }
            }
            ViewBag.ImgPath = talks.TalkImagePath;
            ViewBag.Images = imageList;
            ViewBag.Content = talks.TalkContent;
            return View();
        }

        #endregion

        #region 组织心得审核
        /// <summary>
        /// 进入心得审核界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult TalksOfAuditing()
        {
            return View();
        }

        /// <summary>
        /// 加载未审核的所有心得
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult GetTalksOfAuditing()
        {
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                
                var pageData = TalksService.GetPageEntities(pageSize,pageIndex, out int total,t => t.Status == delAuditing&& t.OrganizeInfoID == LoginOrganize.OrganizeInfoID && t.UserInfoID != null,u=>u.TalkID,true).Select(t => new { t.TalkID, t.UserInfoID, t.UserInfo.UserInfoShowName, t.OrganizeInfoID, t.OrganizeInfo.OrganizeInfoShowName, t.ModfiedOn, t.TalkContent, t.Status }).AsQueryable();
                var data = new { total = total, rows = pageData.ToList() };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var pageData = TalksService.GetPageEntities(pageSize, pageIndex, out int total, t => t.Status == delAuditing && t.UserInfoID != null, u => u.TalkID, true).Select(t => new { t.TalkID, t.UserInfoID, t.UserInfo.UserInfoShowName, t.OrganizeInfoID, t.OrganizeInfo.OrganizeInfoShowName, t.ModfiedOn, t.TalkContent, t.Status }).AsQueryable();

                //var pageData = TalksService.GetEntities(t => t.Status == delAuditing).Select(t => new { t.TalkID, t.UserInfoID, t.UserInfo.UserInfoShowName, t.OrganizeInfoID, t.OrganizeInfo.OrganizeInfoShowName, t.ModfiedOn, t.TalkContent, t.Status }).AsQueryable();
                var data = new { total =total, rows = pageData.ToList() };
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        #region 评论审核
        /// <summary>
        /// 批量删除审核的心得
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult DeleteOfList(String ids)
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
            if (TalksService.InvalidListByULS(idList))
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
        }

        /// <summary>
        /// 批量审核心得
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult EditOfList(String ids)
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
            if (TalksService.NormalListByULS(idList))
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }

        }

        #endregion

        #endregion

        #region 发布心得
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public ActionResult Create(Talks talks)
        {
            if (talks.TalkContent.Length > 300 || talks.TalkContent.Length < 6)
            {
                return Content("fail");
            }
            talks.UserInfoID = LoginUser.UserInfoID;
            talks.TalkFavorsNum = 0;
            talks.CreateTime = DateTime.Now;
            talks.ModfiedOn = DateTime.Now;
            talks.Status = delAuditing;
            talks.OrganizeInfoID = LoginUser.OrganizeInfoID;
            if (TalksService.Add(talks)!=null)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        /// <summary>
        /// 心得图片上传
        /// </summary>
        public ActionResult UploadImage()
        {         
            try
            {
                String filePath = System.Configuration.ConfigurationManager.AppSettings["DefaultTalkImagesSavePath"];
                var file = Request.Files["imagefile"];
                string path = Request["path"];
                if (String.IsNullOrEmpty(path))
                {
                    path = filePath + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + Common.Encryption.MD5Helper.Get_MD5(DateTime.Now.Millisecond.ToString()).Substring(0, 10)+"/";
                }
                string dirPath = Request.MapPath(path);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string fileName = path + Guid.NewGuid().ToString().Substring(1, 5) + "-" + file.FileName+".jpg";
                file.SaveAs(Request.MapPath(fileName));
                return Json(new { src = fileName, msg = "success", path = path }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new {  msg = "fail" }, JsonRequestBehavior.AllowGet);
            }
        }
        
        #endregion

        #region Edit

        public ActionResult Edit(int id)
        {
            //TODO:加载编辑对话框
            Talks talks = TalksService.GetEntities(p => p.TalkID == id && p.Status == delNormal).FirstOrDefault();
            return View(talks);
        }

      
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Talks talks)
        {
            //TODO:Test
            var temp = TalksService.GetEntities(u => u.TalkID == talks.TalkID).FirstOrDefault();
            temp.TalkContent = talks.TalkContent;
            temp.Status = delAuditing;
            string result = String.Empty;
            if (TalksService.Update(temp))
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

        #region 评论删除
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult Delete()
        {
            //if (string.IsNullOrEmpty(ids))
            //{
            //    return Content("Please Select!");
            //}
            //string[] strIds = Request["ids"].Split(',');
            //List<int> idList = new List<int>();
            //foreach (var strId in strIds)
            //{
            //    idList.Add(int.Parse(strId));
            //}
            //if (TalksService.DeleteListByLogical(idList))
            //{
            //    return Content("ok");
            //}
            //else
            //{
            //    return Content("error");
            //}
            int tId = Convert.ToInt32(Request["ids"]);
            Talks talks = TalksService.GetEntities(u => u.TalkID == tId).FirstOrDefault();
            if (talks == null)
            {
                return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                 talks.Status = delDeleted;
                if (TalksService.Update(talks))
                    return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
            }

        } 
        #endregion
    }
}