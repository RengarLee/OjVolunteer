using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Filters
{
    public class LoginCheckFilterAttribute: ActionFilterAttribute
    {
        public Boolean BoolCheckLogin { get; set; }
        public UserInfo LoginUser { get; set; }
        public OrganizeInfo LoginOrganize { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (BoolCheckLogin)
            {
                if (filterContext.HttpContext.Request.Cookies["userLoginId"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Login/Index");
                    base.OnActionExecuting(filterContext);
                    return;
                }
                String userLoginId = filterContext.HttpContext.Request.Cookies["userLoginId"].Value;

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
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}