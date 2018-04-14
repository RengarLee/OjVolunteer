using OjVolunteer.UIPortal.OtherClass;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            
           
            filters.Add(new ExceptionLogFilterAttribute());
        }
    }
}
