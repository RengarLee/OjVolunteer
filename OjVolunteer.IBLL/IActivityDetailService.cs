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
        List<TopView> GetTop(int OrdId, int DateTime,int pageSize, int pageIndex);
    }
}
