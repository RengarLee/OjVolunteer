using Microsoft.VisualStudio.TestTools.UnitTesting;
using OjVolunteer.BLL;
using OjVolunteer.UIPortal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.UIPortal.Controllers.Tests
{
    [TestClass()]
    public class ActivityDetailControllerTests
    {
        ActivityDetailService ActivityDetailService = new ActivityDetailService();
        [TestMethod()]
        public void TopDataTest()
        {
            //组织
            //int OrgId = int.Parse(Request["OrganizeInfoId"] ?? "-1");
            int OrgId = -1;
            //时间  全部  年 季度 月
            //int TimeSpan = int.Parse(Request["TimeSpan"] ?? "-1");
            int TimeSpan = -1;



        }
    }
}