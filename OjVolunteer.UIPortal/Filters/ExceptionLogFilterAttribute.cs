using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.OtherClass
{
    public class ExceptionLogFilterAttribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Common.LogWriter.LogHelper.WriteLog(filterContext.Exception.ToString());
        }
    }
}