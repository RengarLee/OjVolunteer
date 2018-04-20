using OjVolunteer.IBLL;
using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class TalksController : Controller
    {

        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        public ITalksService TalksService { get; set; }
        // GET: Talks
        public ActionResult Index()
        {
            return View();
        }

        #region  加载所有心得 
        public ActionResult GetAllTalks()
        {
            //TODO:分页使用  BS Table
            return View();
        }
        #endregion

        #region Add
        public ActionResult Add()
        {
            //TODO:打开添加对话框
            return View();
        }

        [HttpPost]
        public ActionResult Add(Talks talks)
        {
            //TODO:Test
            talks.CreateTime = DateTime.Now;
            talks.ModfiedOn = DateTime.Now;
            talks.Status = delAuditing;
            talks.TalkFavorsNum = 0;
            if (TalksService.Update(talks))
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
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

        #region Delete
        public ActionResult Delete(String ids)
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
            //批量删除
            #region 逻辑删除
            if (TalksService.DeleteListByLogical(idList) > 0)
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

        #region 图片上传
        public ActionResult UploadImage()
        {
            try
            {
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
            catch (Exception e)
            {
                return Json(new { msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}