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
        private List<ActTopView>  GetData(int OrgId, DateTime actStart, DateTime actEnd, out int total)
        {
            short delNormal = (short)Model.Enum.DelFlagEnum.Normal;

            #region 缓存
            //if (TimeType == 1)//月排行
            //{
            //    list = Common.Cache.CacheHelper.GetCache("ActMonthTop") as List<ActTopView>;
            //    if (list == null)
            //    {
            //        dateTime = DateTime.Now.Month;
            //        var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime.Value.Month == dateTime).AsQueryable();
            //        list = (from u in Data
            //                group u by u.UserInfoId into grouped
            //                orderby grouped.Sum(m => m.ActivityDetailTime) descending, grouped.Key
            //                select new ActTopView { UserInfoID = grouped.Key, ActivityTime = grouped.Sum(m => m.ActivityDetailTime) }).ToList();
            //        int id;
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            id = list[i].UserInfoID;
            //            var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == id).FirstOrDefault();
            //            list[i].ShowName = t.UserInfoShowName;
            //            list[i].OrgId = t.OrganizeInfoID;
            //            list[i].Icon = t.UserInfoIcon;
            //        }
            //        Common.Cache.CacheHelper.SetCache("ActMonthTop", list, DateTime.Now.AddDays(1));
            //    }
            //}
            //else//年排行
            //{
            //    list = Common.Cache.CacheHelper.GetCache("ActYearTop") as List<ActTopView>;
            //    if (list == null)
            //    {
            //        dateTime = DateTime.Now.Year;
            //        var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime.Value.Year == dateTime).AsQueryable();
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

            List<ActTopView> list = Common.Cache.CacheHelper.GetCache("ActTop") as List<ActTopView>;
            if (list == null)
            {
                list = CurrentDal.GetEntities(u => u.Status == delNormal && u.UserInfoId == u.UserInfo.UserInfoID)
                    .Select(n => new ActTopView { UserInfoID = n.UserInfoId, ActivityTime = n.ActivityDetailTime,
                                                OrgId = n.UserInfo.OrganizeInfoID,CreateTime = (DateTime)n.CreateTime})
                   .ToList() ;
                Common.Cache.CacheHelper.SetCache("ActTop", list, DateTime.Now.AddDays(1));
            }

            //组织
            if (OrgId != -1)
            {
                list = list.Where(u => u.OrgId == OrgId).ToList();
            }

            //排序
            list = list.GroupBy(q => q.UserInfoID).Select(q => new ActTopView
            {
                UserInfoID = q.Key,
                ActivityTime = q.Sum(i => i.ActivityTime),
            }).OrderByDescending(i => i.ActivityTime).ToList();
            
            //总人数
            total = list.Count();

            return list;
        }

        //查找用户信息
        private List<ActTopView> GetUser(List<ActTopView> list)
        {
            List<int> Ids = new List<int>();
            foreach (var i in list)
            {
                Ids.Add(i.UserInfoID);
            }
            var models = DbSession.UserInfoDal.GetEntities(u => Ids.Contains(u.UserInfoID)).Select(n => new { n.UserInfoShowName, n.UserInfoIcon, n.UserInfoID }).ToList();
            foreach (var i in list)
            {
                var model = models.Where(u => u.UserInfoID == i.UserInfoID).FirstOrDefault();
                i.Icon = model.UserInfoIcon;
                i.ShowName = model.UserInfoShowName;
            }
            return list;
        }

        //排行榜
        public List<ActTopView> GetTopCache(int OrgId, DateTime actStart,DateTime actEnd, int pageSize, int pageIndex, out int total)
        {
            var list = GetData(OrgId, actStart, actEnd, out total).ToList();
            list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            list = GetUser(list);
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
        public int GetRankCache(int userId, int OrgId, DateTime actStart, DateTime actEnd, out decimal time)
        {
            
            var list = GetData(OrgId, actStart, actEnd, out int total);
            var length = list.Count();
            var i = 0;

            while (i<length)
            {
                if (list[i].UserInfoID == userId)
                {
                    time = list[i].ActivityTime;
                    return i + 1;
                }
                i++;
            }

            //用户未在榜
            time = 0;
            return 0;
        }
    }
}


