using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.UIPortal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class PoliticalController : BaseController
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IPoliticalService PoliticalService { get; set; }


        public ActionResult Index()
        {
            return View(LoginOrganize);
        }

        #region  Query
        /// <summary>
        /// 组织进入政治面貌信息管理界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
        public ActionResult AllPolitical()
        {
            return View(LoginOrganize);
        }
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
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

        #region Create

        [HttpPost]
        public ActionResult Create(Political political)
        {
            if (ModelState.IsValid)
            {
                if (PoliticalService.GetEntities(p => p.PoliticalName.Equals(political.PoliticalName)&& p.Status == delNormal).Count() > 0)
                {
                    return Content("exist");
                }
                political.CreateTime = DateTime.Now;
                political.ModfiedOn = DateTime.Now;
                political.Status = delNormal;
                if (PoliticalService.Add(political) != null)
                {
                    return Content("success");
                }
            }
            return Content("fail");
        }
         
        #endregion

        #region Edit


        [HttpPost]
        public ActionResult Edit(Political political)
        {
            string result = String.Empty;
            political.ModfiedOn = DateTime.Now;
            Political temp = PoliticalService.GetEntities(d => d.PoliticalName.Equals(political.PoliticalName) && d.Status == delNormal).FirstOrDefault();
            if (temp != null)
            {
                result = ("exist");
            }
            else
            {
                if (PoliticalService.Update(political))
                {
                    result = "success";
                }
                else
                {
                    result = "fail";
                }
            }
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete
        public ActionResult DeleteOfList(string ids)
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
            if (PoliticalService.DeleteListByLogical(idList) > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }
        #endregion
    }
}