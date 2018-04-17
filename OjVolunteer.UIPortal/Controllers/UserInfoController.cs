using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class UserInfoController : UserBaseController
    {
        short delNormal = (short)DelFlagEnum.Normal;
        public IUserInfoService UserInfoService { get; set; }
        public IUserDurationService UserDurationService { get; set; }
        // GET: UserInfo
        public ActionResult Index()
        {
            UserInfo user = LoginUser;
            return View(user);
        }

        #region 获得用户信息
        /// <summary>
        /// 用户获得用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetUser(int Id)
        {

            bool isSelf = LoginUser.UserInfoID == Id ? true : false;
            //ViewData["UserDuration"] = "";
            UserDuration userDuration = UserDurationService.GetEntities(u => u.UserDurationID == Id).FirstOrDefault();
            if (userDuration != null)
            {
                ViewData["UserDuration"] = userDuration;
            }
            ViewBag.isSelf = isSelf;
            if(isSelf)
            {
                return View(LoginUser);
            }
            else
            {
                UserInfo user = UserInfoService.GetEntities(u => u.UserInfoID == Id && u.Status == delNormal).FirstOrDefault();
                if (user == null)
                {
                    return View("Shared/Error.cshtml");
                }
                return View(user);
            }
        }


        #endregion


        #region Add
        public ActionResult Add()
        {
            //TODO:打开添加对话框
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserInfo userInfo)
        {
            //TODO:Test
            userInfo.CreateTime = DateTime.Now;
            userInfo.ModfiedOn = DateTime.Now;
            userInfo.Status = delNormal;
            return Content("ok");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            //TODO:加载编辑对话框
            UserInfo userInfo = UserInfoService.GetEntities(p => p.UserInfoID == id && p.Status == delNormal).FirstOrDefault();
            return View(userInfo);
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            //TODO:Test
            string result = String.Empty;
            if (UserInfoService.Update(userInfo))
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
            if (UserInfoService.DeleteListByLogical(idList) > 0)
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