using Microsoft.VisualStudio.TestTools.UnitTesting;
using OjVolunteer.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.BLL.Tests
{
    [TestClass()]
    public class ActivityDetailServiceTests
    {
        ActivityDetailService activity = new ActivityDetailService();
        [TestMethod()]
        public void GetTopTest()
        {
            var temp = activity.GetTop(-1, 6, 3, 1);
        }
    }
}