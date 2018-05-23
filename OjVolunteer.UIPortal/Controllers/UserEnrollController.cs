using OjVolunteer.IBLL;
using OjVolunteer.Model;
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
        public IActivityService ActivityService { get; set; }
        public IUserEnrollService UserEnrollService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 活动报名
        /// </summary>
        /// <param name="activityId">活动Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Enroll(int activityId)
        {
            string msg = String.Empty;
            //TODO:用户验证参加规则
            //TODO:验证用户是否已参加活动
            if (UserEnrollService.GetEntities(u => u.UserInfoID == LoginUser.UserInfoID && u.ActivityID == activityId).Count() > 0)
            {
                return Json(new { msg="您已报名" }, JsonRequestBehavior.AllowGet);
            }
            UserEnroll userEnroll = new UserEnroll { ActivityID = activityId, UserInfoID = LoginUser.UserInfoID, UserEnrollStart = DateTime.Now, Status = delNormal};
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

        /// <summary>
        /// 活动签到
        /// </summary>
        /// <returns></returns>
        public JsonResult SignIn()
        {
            int activityId = Convert.ToInt32(Request["aid"]);
            string[] strIds = Request["ids"].Split(',');
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
            return Json(new { msg}, JsonRequestBehavior.AllowGet);
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