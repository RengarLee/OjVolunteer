using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class BaseController : Controller
    {
        public bool boolCheckLogin = true;
        public UserInfo LoginUser { get; set; }
        public OrganizeInfo LoginOrganize { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (boolCheckLogin)
            {
                if (Request.Cookies["userLoginId"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Login/Index");
                    base.OnActionExecuting(filterContext);
                    return;
                }
                String userLoginId = Request.Cookies["userLoginId"].Value;

                if (Common.Cache.CacheHelper.GetCache(userLoginId) is UserInfo)
                {
                    LoginUser = Common.Cache.CacheHelper.GetCache(userLoginId) as UserInfo;

                }
                else if (Common.Cache.CacheHelper.GetCache(userLoginId) is OrganizeInfo)
                {
                    LoginOrganize = Common.Cache.CacheHelper.GetCache(userLoginId) as OrganizeInfo;
                }

                //缓存过期

                if (LoginUser == null && LoginOrganize == null)
                {
                    filterContext.Result = new RedirectResult("~/Login/Index");
                    base.OnActionExecuting(filterContext);
                    return;
                }
                if (LoginUser != null)
                {
                    Common.Cache.CacheHelper.SetCache(userLoginId, LoginUser, DateTime.Now.AddMinutes(40));
                }
                else
                {
                    Common.Cache.CacheHelper.SetCache(userLoginId, LoginOrganize, DateTime.Now.AddMinutes(40));
                }
                base.OnActionExecuting(filterContext);
                return;
            }
        }

        //更新缓存中的数据
        protected void UpSessionUserInfo(Object value)
        {
            String userLoginId = Request.Cookies["userLoginId"].Value;
            Common.Cache.CacheHelper.SetCache(userLoginId, value);
        }
    }
}