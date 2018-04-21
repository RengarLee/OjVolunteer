using OjVolunteer.IBLL;
using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class PoliticalController : OrganizeBaseController
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IPoliticalService PoliticalService { get; set; }
        // GET: Political
        public ActionResult Index()
        {
            return View();
        }

        #region  加载所有政治面貌 
        public ActionResult AllPolitical()
        {
            return View(LoginUser);
        }

        public ActionResult GetAllPolitical()
        {
            var total = 0;
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = PoliticalService.GetPageEntities(pageSize, pageIndex, out total, o => o.Status == delNormal, u => u.PoliticalID, true).Select(u => new { u.PoliticalName, u.ModfiedOn, u.PoliticalID }).ToList();
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
        public ActionResult Add(String name)
        {
            Political major = PoliticalService.GetEntities(p =>p.PoliticalName.Equals(name)).FirstOrDefault();
            if (major != null)
            {
                return Content("exist");
            }
            major = new Political
            {
                PoliticalName = name,
                CreateTime = DateTime.Now,
                ModfiedOn = DateTime.Now,
                Status = delNormal
            };
            if (PoliticalService.Add(major) != null)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            //TODO:加载编辑对话框
            Political political = PoliticalService.GetEntities(p => p.PoliticalID == id && p.Status == delNormal).FirstOrDefault();
            return View(political);
        }

        [HttpPost]
        public ActionResult Edit(Political political)
        {

            string result = String.Empty;
            Political temp = PoliticalService.GetEntities(p => p.PoliticalID == political.PoliticalID).FirstOrDefault();
            temp.PoliticalName = political.PoliticalName;
            temp.ModfiedOn = DateTime.Now;
            if (PoliticalService.Update(temp))
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
            if (PoliticalService.DeleteListByLogical(idList) > 0)
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
    }
}