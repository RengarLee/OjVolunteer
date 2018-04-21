using OjVolunteer.IBLL;
using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class DepartmentController : OrganizeBaseController
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IDepartmentService DepartmentService { get; set; }
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }

        #region  加载所有院系
        public ActionResult AllDepartment()
        {
            return View(LoginUser);
        }

        public ActionResult GetAllDepartment()
        {
            var total = 0;
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = DepartmentService.GetPageEntities(pageSize, pageIndex, out total, o => o.Status == delNormal, u => u.DepartmentID, true).Select(u => new { u.DepartmentName, u.ModfiedOn, u.DepartmentID }).ToList();
            var data = new { total = total, rows = pageData };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Add
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string name)
        {
            Department department = DepartmentService.GetEntities(p => p.DepartmentName.Equals(name)).FirstOrDefault();
            if (department != null)
            {
                return Content("exist");
            }
            department = new Department
            {
                DepartmentName = name,
                CreateTime = DateTime.Now,
                ModfiedOn = DateTime.Now,
                Status = delNormal
            };
            if (DepartmentService.Add(department) != null)
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
            Department department = DepartmentService.GetEntities(p => p.DepartmentID == id && p.Status == delNormal).FirstOrDefault();
            return View(department);
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            //TODO:Test
            string result = String.Empty;
            if (DepartmentService.Update(department))
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
            if (DepartmentService.DeleteListByLogical(idList) > 0)
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