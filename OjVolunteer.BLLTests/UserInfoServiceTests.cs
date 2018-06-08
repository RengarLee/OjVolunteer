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
    public class UserInfoServiceTests
    {
        [TestMethod()]
        public void SearchUserTest()
        {
            String key = "华";
            UserInfoService userInfoService = new UserInfoService();
            var temp = userInfoService.SearchUser(key);
            return;
        }
    }
}