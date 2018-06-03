using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.UIPortal.Filters;
using OjVolunteer.UIPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class UserEnrollController : BaseController
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        short delUndone = (short)Model.Enum.DelFlagEnum.Undone;
        public IActivityService ActivityService { get; set; }
        public IUserEnrollService UserEnrollService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region 活动报名
        /// <summary>
        /// 活动报名
        /// </summary>
        /// <param name="activityId">活动Id</param>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public JsonResult Create()
        {
            int activityId = Convert.ToInt32(Request["activityId"]);
            string msg = String.Empty;

            if (LoginUser.Status == delAuditing)
            {
                return Json(new { msg = "您的政治面貌尚未审核，无法参加活动，请耐心等待！" }, JsonRequestBehavior.AllowGet);
            }
            //已报名
            if (UserEnrollService.GetEntities(u => u.UserInfoID == LoginUser.UserInfoID && u.ActivityID == activityId).Count() > 0)
            {
                return Json(new { msg = "您已报名" }, JsonRequestBehavior.AllowGet);
            }

            Activity activity = ActivityService.GetEntities(u => u.ActivityID == activityId && u.Status == delUndone && u.ActivityPolitical.Contains("," + LoginUser.PoliticalID + ",") && u.ActivityMajor.Contains("," + LoginUser.MajorID + ",") && u.ActivityDepartment.Contains("," + LoginUser.DepartmentID + ",")).FirstOrDefault();
            //报名条件
            if (activity == null)
            {
                return Json(new { msg = "报名失败,请稍后再试" }, JsonRequestBehavior.AllowGet);
            }
            if (activity.ActivityEnrollEnd < DateTime.Now)
            {
                return Json(new { msg = "报名已结束" }, JsonRequestBehavior.AllowGet);
            }
            UserEnroll userEnroll = new UserEnroll { ActivityID = activityId, UserInfoID = LoginUser.UserInfoID, UserEnrollStart = DateTime.Now, Status = delNormal };
            if (UserEnrollService.Add(userEnroll) != null)
            {
                msg = "报名成功";
            }
            else
            {
                msg = "报名失败,请稍后再试";
            }
            return Json(new { msg }, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region 列表活动签到签退

        public ActionResult SingIn(int aId)
        {
            ViewBag.ActivityId = aId;
            return View();
        }

        public JsonResult SingInData()
        {
            int typeId =Convert.ToInt32(Request["typeId"]);
            int activityId =Convert.ToInt32(Request["activityId"]);
            var data = UserEnrollService.GetEntities(u => u.ActivityID == activityId).AsQueryable();
            //未签到
            if (typeId == 1)
            {
                data = data.Where(u=>u.Status == delAuditing).AsQueryable();
            }
            if (typeId == 2)
            {
                data = data.Where(u => u.Status != delAuditing).AsQueryable();
            }
            List<SingModel> list = new List<SingModel>();
            foreach (var temp in data)
            {
                SingModel sing = new SingModel()
                {
                    SingTime = temp.UserEnrollActivityStart,
                    ShowName = temp.UserInfo.UserInfoShowName,
                    LoginId = temp.UserInfo.UserInfoLoginId,
                    UserInfoId = temp.UserInfoID,
                };
                if (temp.Status == delAuditing)
                {
                    sing.isSing = true;
                }
                else
                {
                    sing.isSing = false;
                }
                list.Add(sing);
            }
            return Json(new { msg="success",data= list },JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 活动签到
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionAuthentication(AbleOrganize = false, AbleUser =true)]
        public JsonResult ListSignIn()
        {
            int activityId = Convert.ToInt32(Request["activityId"]);
            string[] strIds = Request["ids"].Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
            string msg = String.Empty;
            List<int> uIdList = new List<int>();
            foreach (var strId in strIds)
            {
                uIdList.Add(int.Parse(strId));
            }
            if (UserEnrollService.SignIn(activityId, uIdList))
            {
                msg = "success";
            }
            else
            {
                msg = "fail";
            }
            return Json(new { msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 活动签退
        /// </summary>
        /// <returns></returns>
        public JsonResult SignOut()
        {
            int activityId = Convert.ToInt32(Request["aid"]);
            string[] strIds = Request["ids"].Split(',');
            string msg = String.Empty;
            List<int> uIdList = new List<int>();
            foreach (var strId in strIds)
            {
                uIdList.Add(int.Parse(strId));
            }
            if (UserEnrollService.SignOut(activityId, uIdList))
            {
                msg = "success";
            }
            else
            {
                msg = "fail";
            }
            return Json(new { msg }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 二维码签到签退
        [ActionAuthentication(AbleOrganize = false, AbleUser = true)]
        public JsonResult QrCodeSignIn()
        {
            int activityId = Convert.ToInt32(Request["aid"]);
            string msg = String.Empty;
            var temp = UserEnrollService.GetEntities(u => u.ActivityID == activityId && u.UserInfoID == LoginUser.UserInfoID).FirstOrDefault();
            if (temp !=null)
            {
                temp.UserEnrollActivityStart = DateTime.Now;
                temp.Status = (short)Model.Enum.DelFlagEnum.Auditing;
                if (UserEnrollService.Update(temp))
                    msg = "success";
                else
                    msg = "fail";
            }
            else
            {
                msg = "noexist";
            }
            return Json(new { msg }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult Edit(int userEnrollId)
        {
            ViewData.Model = UserEnrollService.GetEntities(u => u.UserEnrollID == userEnrollId).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public JsonResult Edit(UserEnroll userEnroll)
        {
            TimeSpan timeSpan = (TimeSpan)(userEnroll.UserEnrollActivityEnd - userEnroll.UserEnrollActivityStart);
            decimal Time = timeSpan.Hours * 60 + timeSpan.Minutes;
            userEnroll.ActivityTime = Time;
            if (UserEnrollService.Update(userEnroll))
            {
                return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}