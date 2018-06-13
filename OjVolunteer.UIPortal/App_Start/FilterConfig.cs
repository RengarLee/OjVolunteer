using OjVolunteer.UIPortal.Filters;
using OjVolunteer.UIPortal.OtherClass;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //异常过滤
            filters.Add(new ExceptionLogFilterAttribute());
            filters.Add(new LoginCheckFilterAttribute() { BoolCheckLogin = true });
        }
    }
}
