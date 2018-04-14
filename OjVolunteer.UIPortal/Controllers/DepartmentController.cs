using OjVolunteer.IBLL;
using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class DepartmentController : Controller
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IDepartmentService DepartmentService { get; set; }
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }

        #region  加载所有院系
        public ActionResult GetAllDepartment()
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
        public ActionResult Add(Department department)
        {
            //TODO:Test
            department.CreateTime = DateTime.Now;
            department.ModfiedOn = DateTime.Now;
            department.Status = delNormal;
            return Content("ok");
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
            if (DepartmentService.DeleteListByLogical(idList) > 0)
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