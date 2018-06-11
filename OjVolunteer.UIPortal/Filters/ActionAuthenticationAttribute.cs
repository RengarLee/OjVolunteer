using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Filters
{
    public class ActionAuthenticationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 团队是否可以执行
        /// </summary>
        public bool AbleOrganize = false;
        /// <summary>
        /// 用户是否可以执行
        /// </summary>
        public bool AbleUser = false;
        /// <summary>
        /// 是否为最高团队才可以执行
        /// </summary>
        public bool Super = false;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AbleUser && AbleOrganize)
            {
            }//用户可以执行 团队可以执行
            else if (AbleUser && !AbleOrganize)
            {
                String userLoginId = filterContext.HttpContext.Request.Cookies["userLoginId"].Value;
                if (Common.Cache.CacheHelper.GetCache(userLoginId) is OrganizeInfo)
                {
                    filterContext.Result = new RedirectResult("~/OrganizeInfo/Index");
                }
            }//用户可以执行 团队不可以执行
            else if (!AbleUser && AbleOrganize)
            {
                String userLoginId = filterContext.HttpContext.Request.Cookies["userLoginId"].Value;
                if (Common.Cache.CacheHelper.GetCache(userLoginId) is UserInfo)
                {
                    filterContext.Result = new RedirectResult("~/UserInfo/Index");
                }
                if (Super)
                {
                    OrganizeInfo organizeInfo = Common.Cache.CacheHelper.GetCache(userLoginId) as OrganizeInfo;
                    if (organizeInfo.OrganizeInfoManageId != null)
                    {
                        filterContext.Result = new RedirectResult("~/OrganizeInfo/Index");
                    }
                }
            }//用户不可以执行 团队可以执行
            else if (!AbleUser && !AbleOrganize)
            {
                String userLoginId = filterContext.HttpContext.Request.Cookies["userLoginId"].Value;
                if (Common.Cache.CacheHelper.GetCache(userLoginId) is UserInfo)
                {
                    filterContext.Result = new RedirectResult("~/UserInfo/Index");

                }
                else
                {
                    filterContext.Result = new RedirectResult("~/OrganizeInfo/Index");
                }
            }//用户不可以执行 团队不可以执行
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}