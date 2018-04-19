using OjVolunteer.IBLL;
using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class MajorController : OrganizeBaseController
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IMajorService MajorService { get; set; }
        // GET: Major
        public ActionResult Index()
        {
            return View();
        }

        #region 加载专业
        public ActionResult AllMajor()
        {
            return View(LoginUser);
        }

        [HttpPost]
        public ActionResult GetAllMajor()
        {
            var total = 0;
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = MajorService.GetPageEntities(pageSize, pageIndex, out total, o => o.Status == delNormal, u => u.MajorID, true).Select(u => new { u.MajorName, u.ModfiedOn, u.MajorID }).ToList();
            var data = new { total = total, rows = pageData };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Add
        public ActionResult Add()
        {
            //TODO:打开添加对话框
            return View();
        }

        [HttpPost]
        public ActionResult Add(Major major)
        {
            //TODO:Test
            major.CreateTime = DateTime.Now;
            major.ModfiedOn = DateTime.Now;
            major.Status = delNormal;
            return Content("ok");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            //TODO:加载编辑对话框
            Major major = MajorService.GetEntities(p => p.MajorID == id && p.Status == delNormal).FirstOrDefault();
            return View(major);
        }

        [HttpPost]
        public ActionResult Edit(Major major)
        {
            //TODO:Test
            string result = String.Empty;
            Major temp = MajorService.GetEntities(p => p.MajorID == major.MajorID).FirstOrDefault();
            temp.MajorName = major.MajorName;
            temp.ModfiedOn = DateTime.Now;
            if (MajorService.Update(temp))
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
            //批量删除
            #region 逻辑删除
            if (MajorService.DeleteListByLogical(idList) > 0)
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
    }
}