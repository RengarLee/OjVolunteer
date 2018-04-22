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
    public class MajorController : BaseController
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IMajorService MajorService { get; set; }
        // GET: Major
        public ActionResult Index()
        {
            return View();
        }

        #region Query
        [ActionAuthentication(AbleOrganize = true, Super = true)]
        public ActionResult AllMajor()
        {
            return View(LoginOrganize);
        }

        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, Super =true)]
        public ActionResult GetAllMajor()
        {
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = MajorService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delNormal, u => u.MajorID, true).Select(u => new { u.MajorName, u.ModfiedOn, u.MajorID }).ToList();
            var data = new { total = total, rows = pageData };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Add

        [HttpPost]
        [ActionAuthentication(AbleOrganize = true , Super = true)]
        public ActionResult Add(String name)
        {

            Major major = MajorService.GetEntities(m => m.MajorName.Equals(name)).FirstOrDefault();
            if (major != null)
            {
                return Content("exist");
            }
            major = new Major
            {
                MajorName = name,
                CreateTime = DateTime.Now,
                ModfiedOn = DateTime.Now,
                Status = delNormal
            };
            if (MajorService.Add(major)!=null)
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

        /// <summary>
        /// 行编辑专业名称
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true,Super =true)]
        public ActionResult Edit(Major major)
        {

            string result = String.Empty;
            major.ModfiedOn = DateTime.Now;
            Major temp = MajorService.GetEntities(d => d.MajorName.Equals(major.MajorName) && d.Status == delNormal).FirstOrDefault();
            if (temp != null)
            {
                result = ("exist");
            }
            else
            {
                if (MajorService.Update(major))
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
        [ActionAuthentication(AbleOrganize = true, Super = true)]
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
            if (MajorService.DeleteListByLogical(idList) > 0)
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