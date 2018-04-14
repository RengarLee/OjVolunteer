using OjVolunteer.IBLL;
using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class OrganizeInfoController : Controller
    {
        // GET: OrganizeInfo
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public IOrganizeInfoService OrganizeInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region  加载所有政治面貌 
        public ActionResult GetAllOrganizeInfo()
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
        public ActionResult Add(OrganizeInfo organizeInfo)
        {
            //TODO:Test
            organizeInfo.CreateTime = DateTime.Now;
            organizeInfo.ModfiedOn = DateTime.Now;
            organizeInfo.Status = delNormal;
            return Content("ok");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            //TODO:加载编辑对话框
            OrganizeInfo organizeInfo = OrganizeInfoService.GetEntities(p => p.OrganizeInfoID == id && p.Status == delNormal).FirstOrDefault();
            return View(organizeInfo);
        }

        [HttpPost]
        public ActionResult Edit(OrganizeInfo organizeInfo)
        {
            //TODO:Test
            string result = String.Empty;
            if (OrganizeInfoService.Update(organizeInfo))
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
            if (OrganizeInfoService.DeleteListByLogical(idList) > 0)
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