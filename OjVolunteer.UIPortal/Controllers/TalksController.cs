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
    public class TalksController : BaseController
    {

        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        short delDeleted = (short)Model.Enum.DelFlagEnum.Deleted;
        short delInvalid = (short)Model.Enum.DelFlagEnum.Invalid;
        public ITalksService TalksService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region  Query

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
        /// 进入心得审核界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult TalksOfAuditing()
        {
            return View(LoginOrganize);
        }

        /// <summary>
        /// 加载未审核的所有心得
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false)]
        public ActionResult GetTalksOfAuditing()
        {
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = TalksService.GetEntities(t => t.Status == delAuditing).Select(t => new {t.TalkID,t.UserInfoID ,t.UserInfo.UserInfoShowName, t.OrganizeInfoID, t.OrganizeInfo.OrganizeInfoShowName, t.ModfiedOn, t.TalkContent, t.Status}).AsQueryable();
            if (LoginOrganize.OrganizeInfoManageId != null)
            {
                pageData = pageData.Where(t => t.OrganizeInfoID == LoginOrganize.OrganizeInfoID && t.UserInfoID == null).AsQueryable();
            }
            var data = new { total = pageData.Count(), rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据用户ID查看该用户发表的已通过审核的心得列表
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = true)]
        public ActionResult GetTalkByUserId()
        {
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            if (String.IsNullOrEmpty(Request["userId"]))
            {
                return Json(new { total = 0, rows = "" }, JsonRequestBehavior.AllowGet);
            }
            int userId = Convert.ToInt32(Request["userId"]);
            var pageData = TalksService.GetPageEntities(pageSize, pageIndex, out int total, u => u.UserInfoID == userId, u => u.TalkID, true).Select(n => new { n.TalkID, n.UserInfo.UserInfoShowName, n.TalkImagePath, n.TalkFavorsNum, n.TalkContent, n.Status, n.CreateTime, n.ModfiedOn }).ToList();

            var data = new { total = total, rows = pageData };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

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
            var pageData = TalksService.GetPageEntities(pageSize, pageIndex, out int total, u => u.OrganizeInfoID == orgId, u => u.TalkID, true).Select(n => new { n.TalkID, n.UserInfo.UserInfoShowName, n.TalkImagePath, n.TalkFavorsNum, n.TalkContent, n.Status, n.CreateTime, n.ModfiedOn }).ToList();

            var data = new { total = total, rows = pageData };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据心得ID查看心得详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = true)]
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

        #region Add
        public ActionResult Add()
        {
            Talks talks = new Talks
            {
                CreateTime = DateTime.Now,
                Status = delInvalid,
                ModfiedOn = DateTime.Now,
                TalkContent = "",
                UserInfoID = LoginUser.UserInfoID,
                OrganizeInfoID = LoginUser.OrganizeInfoID,
                
                TalkFavorsNum = 0,
            };
            talks = TalksService.Add(talks);
            ViewBag.TalkId = talks.TalkID;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Talks talks)
        {
            talks.CreateTime = DateTime.Now;
            talks.ModfiedOn = DateTime.Now;
            talks.Status = delAuditing;
            talks.TalkFavorsNum = 0;
            if (TalksService.Update(talks))
            {
                return Content("success");
            }
            else
            {
                return Content("ok");
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

        #region Delete
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
        #endregion

        #region 图片上传
        public ActionResult UploadImage()
        {
            //TODO:添加异常
            var file = Request.Files["file"];
                int id = Convert.ToInt32(Request["id"]);
                string path = "/Content/Upload/TalkImages/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + id+"/";
                string dirPath = Request.MapPath(path);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                    Talks talks = TalksService.GetEntities(u => u.TalkID == id).FirstOrDefault();
                    talks.TalkImagePath = path;
                    TalksService.Update(talks);
                }
                string fileName = path + Guid.NewGuid().ToString().Substring(1, 5) + "-" + file.FileName;
                file.SaveAs(Request.MapPath(fileName));
                return Json(new { src = fileName, msg = "ok" }, JsonRequestBehavior.AllowGet);

        }
        #endregion

    }
}