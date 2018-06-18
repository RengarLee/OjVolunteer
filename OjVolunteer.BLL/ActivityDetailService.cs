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
        //获得数据
        private List<ActTopView> GetData(int OrdId, int TimeType)
        {
            List<ActTopView> list = null;
            int dateTime;
            short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
            #region 滑动排行
            //if (TimeType == 1)//月排行
            //{
            //    list = Common.Cache.CacheHelper.GetCache("ActMonthTop") as List<ActTopView>;
            //    if (list == null)
            //    {
            //        dateTime = DateTime.Now.AddDays(-1 * 30);
            //        var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime > dateTime).AsQueryable();
            //        list = (from u in Data
            //                group u by u.UserInfoId into grouped
            //                orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
            //                select new ActTopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
            //        foreach (var temp in list)
            //        {
            //            var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == temp.UserInfoID).FirstOrDefault();
            //            temp.ShowName = t.UserInfoShowName;
            //            temp.OrgId = t.OrganizeInfoID;
            //            temp.Icon = t.UserInfoIcon;
            //        }
            //        Common.Cache.CacheHelper.SetCache("ActMonthTop", list, DateTime.Now.AddDays(1));
            //    }

            //}
            //if (TimeType == 2)//季排行
            //{
            //    list = Common.Cache.CacheHelper.GetCache("ActSeasonTop") as List<ActTopView>;
            //    if (list == null)
            //    {
            //        dateTime = DateTime.Now.AddDays(-1 * 90);
            //        var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime > dateTime).AsQueryable();
            //        list = (from u in Data
            //                group u by u.UserInfoId into grouped
            //                orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
            //                select new ActTopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
            //        foreach (var temp in list)
            //        {
            //            var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == temp.UserInfoID).FirstOrDefault();
            //            temp.ShowName = t.UserInfoShowName;
            //            temp.OrgId = t.OrganizeInfoID;
            //            temp.Icon = t.UserInfoIcon;

            //        }
            //        Common.Cache.CacheHelper.SetCache("ActSeasonTop", list, DateTime.Now.AddDays(1));
            //    }

            //}
            //if (TimeType == 3)//年排行
            //{
            //    list = Common.Cache.CacheHelper.GetCache("ActYearTop") as List<ActTopView>;
            //    if (list == null)
            //    {
            //        dateTime = DateTime.Now.AddDays(-1 * 365);
            //        var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime > dateTime).AsQueryable();
            //        list = (from u in Data
            //                group u by u.UserInfoId into grouped
            //                orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
            //                select new ActTopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
            //        foreach (var temp in list)
            //        {
            //            var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == temp.UserInfoID).FirstOrDefault();
            //            temp.ShowName = t.UserInfoShowName;
            //            temp.OrgId = t.OrganizeInfoID;
            //            temp.Icon = t.UserInfoIcon;
            //        }
            //        Common.Cache.CacheHelper.SetCache("ActYearTop", list, DateTime.Now.AddDays(1));
            //    }
            //}
            #endregion

            #region 缓存
            if (TimeType == 1)//月排行
            {
                list = Common.Cache.CacheHelper.GetCache("ActMonthTop") as List<ActTopView>;
                if (list == null)
                {
                    dateTime = DateTime.Now.Month;
                    var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime.Value.Month == dateTime).AsQueryable();
                    list = (from u in Data
                            group u by u.UserInfoId into grouped
                            orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
                            select new ActTopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
                    int id;
                    for (int i = 0; i < list.Count; i++)
                    {
                        id = list[i].UserInfoID;
                        var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == id).FirstOrDefault();
                        list[i].ShowName = t.UserInfoShowName;
                        list[i].OrgId = t.OrganizeInfoID;
                        list[i].Icon = t.UserInfoIcon;
                    }
                    Common.Cache.CacheHelper.SetCache("ActMonthTop", list, DateTime.Now.AddDays(1));
                }
            }
            else//年排行
            {
                list = Common.Cache.CacheHelper.GetCache("ActYearTop") as List<ActTopView>;
                if (list == null)
                {
                    dateTime = DateTime.Now.Year;
                    var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime.Value.Year == dateTime).AsQueryable();
                    list = (from u in Data
                            group u by u.UserInfoId into grouped
                            orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
                            select new ActTopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
                    foreach (var temp in list)
                    {
                        var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == temp.UserInfoID).FirstOrDefault();
                        temp.ShowName = t.UserInfoShowName;
                        temp.OrgId = t.OrganizeInfoID;
                        temp.Icon = t.UserInfoIcon;
                    }
                    Common.Cache.CacheHelper.SetCache("ActYearTop", list, DateTime.Now.AddDays(1));
                }
            }
            #endregion

            #region 无缓存
            //if (TimeType == 1)//月排行
            //{
            //    dateTime = DateTime.Now.Month;
            //    var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime.Value.Month == dateTime).AsQueryable();
            //    list = (from u in Data
            //            group u by u.UserInfoId into grouped
            //            orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
            //            select new ActTopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
            //    
            //}
            //else//年排行
            //{
            //    dateTime = DateTime.Now.Year;
            //    var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime.Value.Year == dateTime).AsQueryable();
            //    list = (from u in Data
            //            group u by u.UserInfoId into grouped
            //            orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
            //            select new ActTopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
            //    int id;
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        id = list[i].UserInfoID;
            //        var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == id).FirstOrDefault();
            //        list[i].ShowName = t.UserInfoShowName;
            //        list[i].OrgId = t.OrganizeInfoID;
            //        list[i].Icon = t.UserInfoIcon;
            //    }
            //} 
            #endregion

            if (OrdId != -1)
            {
                list = list.Where(u => u.OrgId == OrdId).ToList();
            }
            return list;
        }

        //排行榜
        public List<ActTopView> GetTop(int OrdId, int TimeType, int pageSize, int pageIndex, out int total)
        {
            List<ActTopView> list = GetData(OrdId, TimeType);
            total = list.Count();
            list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return list;
        }

        //获得排名
        /// <summary>
        /// 返回用户Rank
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="OrgId">团队Id</param>
        /// <param name="TimeType">时间类别</param>
        /// <param name="time">志愿者时长</param>
        /// <returns></returns>
        public int GetRank(int userId, int OrgId, int TimeType, out decimal time)
        {
            int i = 0, length;
            List<ActTopView> list = GetData(OrgId, TimeType);
            length = list.Count();
            for(;i<length;i++)
            {
                if (list[i].UserInfoID == userId)
                {
                    time = list[i].ActivityTime;
                    return i + 1;
                }
            }

            //用户未在榜
            time = 0;
            return 0;
        }
    }
}


