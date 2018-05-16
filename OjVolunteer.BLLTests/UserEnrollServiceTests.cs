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
    public class UserEnrollServiceTests
    {
        UserEnrollService Service = new UserEnrollService();
        [TestMethod()]
        public void SignInTest()
        {
            int aId = 2;
            List<int> uIdList = new List<int> { 2, 3 };
            Service.SignIn(aId, uIdList);
        }
    }
}