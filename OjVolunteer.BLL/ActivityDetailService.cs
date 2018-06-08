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
    public partial class ActivityDetailService : BaseService<ActivityDetail>, IActivityDetailService
    {
        //排行榜
        public List<TopView> GetTop(int OrdId, int TimeType, int pageSize, int pageIndex)
        {
            List<TopView> list = null;
            DateTime dateTime;
            short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
            if (TimeType == 1)//月排行
            {
                list = Common.Cache.CacheHelper.GetCache("ActMonthTop") as List<TopView>;
                if (list == null)
                {
                    dateTime = DateTime.Now.AddDays(-1 * 30);
                    var Data = CurrentDal.GetEntities(u => u.Status == delNormal&&u.CreateTime>dateTime).AsQueryable();
                    list = (from u in Data
                                group u by u.UserInfoId into grouped
                                orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
                                select new TopView {UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
                    foreach (var temp in list)
                    {
                        var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == temp.UserInfoID).FirstOrDefault();
                        temp.ShowName = t.UserInfoShowName;
                        temp.OrgId = t.OrganizeInfoID;
                        temp.Icon = t.UserInfoIcon;
                    }
                    Common.Cache.CacheHelper.SetCache("ActMonthTop", list, DateTime.Now.AddMinutes(20));
                }
                
            }
            if (TimeType == 2)//季排行
            {
                   list = Common.Cache.CacheHelper.GetCache("ActSeasonTop") as List<TopView>;
                if (list == null)
                {
                    dateTime = DateTime.Now.AddDays(-1 * 90);
                    var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime > dateTime).AsQueryable();
                    list = (from u in Data
                                group u by u.UserInfoId into grouped
                                orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
                                select new TopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
                    foreach (var temp in list)
                    {
                        var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == temp.UserInfoID).FirstOrDefault();
                        temp.ShowName = t.UserInfoShowName;
                        temp.OrgId = t.OrganizeInfoID;
                        temp.Icon = t.UserInfoIcon;

                    }
                    Common.Cache.CacheHelper.SetCache("ActSeasonTop", list, DateTime.Now.AddMinutes(20));
                }

            }
            if (TimeType == 3)//年排行
            {
                list = Common.Cache.CacheHelper.GetCache("ActAllTop") as List<TopView>;
                if (list == null)
                {
                    dateTime = DateTime.Now.AddDays(-1 * 365);
                    var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime > dateTime).AsQueryable();
                    list = (from u in Data
                                group u by u.UserInfoId into grouped
                                orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
                                select new TopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
                    foreach (var temp in list)
                    {
                        var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == temp.UserInfoID).FirstOrDefault();
                        temp.ShowName = t.UserInfoShowName;
                        temp.OrgId = t.OrganizeInfoID;
                        temp.Icon = t.UserInfoIcon;
                    }
                    Common.Cache.CacheHelper.SetCache("ActAllTop", list, DateTime.Now.AddMinutes(20));
                }
            }
            if (TimeType == 4)//排行
            {
                list = Common.Cache.CacheHelper.GetCache("ActAllTop") as List<TopView>;
                if (list == null)
                {
                    var Data = CurrentDal.GetEntities(u => u.Status == delNormal).AsQueryable();
                    list = (from u in Data
                            group u by u.UserInfoId into grouped
                            orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
                            select new TopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
                    foreach (var temp in list)
                    {
                        var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == temp.UserInfoID).FirstOrDefault();
                        temp.ShowName = t.UserInfoShowName;
                        temp.OrgId = t.OrganizeInfoID;
                        temp.Icon = t.UserInfoIcon;
                    }
                    Common.Cache.CacheHelper.SetCache("ActAllTop", list, DateTime.Now.AddMinutes(20));
                }
            }

            if (OrdId != -1)
            {
                list = list.Where(u => u.OrgId == OrdId).ToList();
            }
            list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return list;
        }

    }
}
