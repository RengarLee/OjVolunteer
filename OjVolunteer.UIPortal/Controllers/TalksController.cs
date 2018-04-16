using OjVolunteer.IBLL;
using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class TalksController : Controller
    {

        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
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
            talks.Status = delNormal;
            return Content("ok");
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
        public ActionResult Edit(Talks talks)
        {
            //TODO:Test
            string result = String.Empty;
            if (TalksService.Update(talks))
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
        public ActionResult Delete(string ids)
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
                return Content("error");
            }
            else
            {
                return Content("ok");
            }
            #endregion
        }
        #endregion
    }
}