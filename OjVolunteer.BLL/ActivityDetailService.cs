using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.BLL
{
    public partial class ActivityDetailService:BaseService<ActivityDetail>,IActivityDetailService
    {
        //public TopView GetTop(int OrdId, int TimeSpan)
        //{
        //    short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        //    var Data = CurrentDal.GetEntities(u => u.Status == delNormal).AsQueryable();
        //    if(OrgId>)
        //}
    }
}
