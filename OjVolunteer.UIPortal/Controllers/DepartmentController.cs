﻿using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.UIPortal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class DepartmentController : BaseController
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IDepartmentService DepartmentService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        #region  Query

        /// <summary>
        /// 组织进入学院信息管理界面
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false,Super =true)]
        public ActionResult AllDepartment()
        {
            return View(LoginOrganize);
        }

        /// <summary>
        /// 加载学院信息
        /// </summary>
        /// <returns></returns>
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super =true)]
        public ActionResult GetAllDepartment()
        { 
            var s = Request["limit"];
            int pageSize = int.Parse(Request["limit"] ?? "5");
            int offset = int.Parse(Request["offset"] ?? "0");
            int pageIndex = (offset / pageSize) + 1;
            var pageData = DepartmentService.GetPageEntities(pageSize, pageIndex, out int total, o => o.Status == delNormal, u => u.DepartmentID, true).Select(u => new { u.DepartmentName, u.ModfiedOn, u.DepartmentID }).ToList();
            var data = new { total = total, rows = pageData };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Add

        /// <summary>
        /// 添加学院信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
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
        /// <summary>
        /// 行内编辑学院名称
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
        public ActionResult Edit(Department department)
        {
            string result = String.Empty;
            department.ModfiedOn = DateTime.Now;
            Department temp = DepartmentService.GetEntities(d => d.DepartmentName.Equals(department.DepartmentName)&&d.Status==delNormal).FirstOrDefault();
            if (temp != null)
            {
                result = ("exist");
            }
            else
            {
                if (DepartmentService.Update(department))
                {
                    result = "success";
                }
                else
                {
                    result = "fail";
                }
            }
            
            return Json(new { result=result},JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete
        [ActionAuthentication(AbleOrganize = true, AbleUser = false, Super = true)]
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
            if (DepartmentService.DeleteListByLogical(idList) > 0)
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