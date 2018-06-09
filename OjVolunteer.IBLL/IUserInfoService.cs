using OjVolunteer.Model;
using OjVolunteer.Model.Param;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.IBLL
{
    public partial interface  IUserInfoService
    {
        Stream ExportToExecl(bool isSuper, int orgId);

        IQueryable<UserInfo> LoadPageData(UserQueryParam userQueryParam);

        bool ListUpdatePolical(List<int> ids);

        bool AddUser(UserInfo userInfo);

        List<UserInfo> SearchUser(String key);

        bool UpdatePassWord(UserInfo user, string oldPwd, string newPwd);
    }
}
