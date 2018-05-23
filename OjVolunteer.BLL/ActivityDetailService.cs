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
        public List<TopView> GetTop(int OrdId, int TimeSpan)
        {
            short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
            var Data = CurrentDal.GetEntities(u => u.Status == delNormal).AsQueryable();
            //筛选组织
            if (OrdId != -1)
            {
                Data = Data.Where(u => u.UserInfo.OrganizeInfoID == OrdId).AsQueryable();
            }
            if (TimeSpan != -1)
            {
                DateTime dateTime = DateTime.Now.AddDays(-1 * TimeSpan);
                Data = Data.Where(u => u.CreateTime > dateTime).AsQueryable();
            }
            List<TopView> tempList = (from u in Data
                        group u by u.UserInfoId into grouped
                        orderby grouped.Sum(m => m.ActivityDetailTime)
                        select new TopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
            foreach (var temp in tempList)
            {
                temp.UserInfoShowName = CurrentDal.GetEntities(u => u.UserInfoId == temp.UserInfoID).FirstOrDefault().UserInfo.UserInfoShowName;
            }
            return tempList;
        }
    }
}
