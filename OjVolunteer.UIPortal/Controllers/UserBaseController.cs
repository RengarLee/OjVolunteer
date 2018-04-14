using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class UserBaseController : Controller
    {
        // GET: Base
        public bool boolCheckLogin = true;
        public UserInfo LoginUser { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (boolCheckLogin)
            {
                //未找到cookie
                if (Request.Cookies["userLoginId"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/Login/Index");
                    return;
                }
                String userLoginId = Request.Cookies["userLoginId"].Value;
                UserInfo userInfo = Common.Cache.CacheHelper.GetCache(userLoginId) as UserInfo;

                //缓存过期
                if (userInfo == null)
                {
                    filterContext.HttpContext.Response.Redirect("/Login/Index");
                    return;
                }

                LoginUser = userInfo;
                //滑动机制
                Common.Cache.CacheHelper.SetCache(userLoginId, userInfo, DateTime.Now.AddMinutes(20));
            }
        }
    }
}