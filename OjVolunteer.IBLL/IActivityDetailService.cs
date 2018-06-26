using OjVolunteer.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.IBLL
{
    public partial interface IActivityDetailService
    {
        List<ActTopView> GetTopCache(int OrdId, DateTime actStart, DateTime actEnd, int pageSize, int pageIndex, out int total);
        int GetRankCache(int userId, int OrgId, DateTime actStart, DateTime actEnd, out decimal time);
    }
}
