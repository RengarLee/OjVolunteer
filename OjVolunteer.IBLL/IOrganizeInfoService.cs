using OjVolunteer.Model;
using OjVolunteer.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.IBLL
{
    public partial interface IOrganizeInfoService : IBaseService<OrganizeInfo>
    {
        IQueryable<OrganizeInfo> LoadPageData(OrganizeQueryParam organizeQueryParam ,int loginUserId);
    }
}
