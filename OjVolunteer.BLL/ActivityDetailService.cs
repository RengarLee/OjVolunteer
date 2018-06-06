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
        public List<TopView> GetTop(int OrdId, int TimeSpan,int pageSize, int pageIndex)
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
            #region Linq升序
            //List<TopView> tempList = (from u in Data
            //                          group u by u.UserInfoId into grouped
            //                          orderby grouped.Sum(m => m.ActivityDetailTime), grouped.Key
            //                          select new TopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }
            //                            ).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(); 
            #endregion

            #region Linq降序
            List<TopView> tempList = (from u in Data
                                      group u by u.UserInfoId into grouped
                                      orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
                                      select new TopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }
                                        ).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            #endregion
            //var tempList = Data.GroupBy(u => u.UserInfoId).OrderByDescending(u => u.Sum(o => o.ActivityDetailTime)).Skip((pageIndex - 1) * pageSize).Take(pageSize).Select((u, idx) => new TopView { NoId = idx, UserInfoID = u.Key, ActivityTime = u.Sum(o => o.ActivityDetailTime) }).ToList();
            int i = 1;
            foreach (var temp in tempList)
            {
                temp.NoId = (pageIndex - 1) * pageSize + i++;
                temp.UserInfoShowName = CurrentDal.GetEntities(u => u.UserInfoId == temp.UserInfoID).FirstOrDefault().UserInfo.UserInfoShowName;
            }
            return tempList;
        }
    }
}
