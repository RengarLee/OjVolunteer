using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Param;
using OjVolunteer.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.BLL
{
    public partial class TalksService: BaseService<Talks>, ITalksService
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        short delInvalid = (short)Model.Enum.DelFlagEnum.Invalid;
        short delDeleted = (short)Model.Enum.DelFlagEnum.Deleted;

        #region 多条件查询
        public IQueryable<Talks> LoadPageData(TalkQueryParam talkQueryParam)
        {
            var temp = DbSession.TalksDal.GetEntities(u => u.Status == delNormal).AsQueryable(); ;

            #region 状态
            //short delFlag = -1;
            //if (!String.IsNullOrEmpty(talkQueryParam.Status))
            //{
            //    if (("正常").Contains(talkQueryParam.Status))
            //    {
            //        delFlag = 0;
            //    }

            //}delNormal
            //if (delFlag > -1)
            //{
            //    temp = temp.Where(u => u.Status == delFlag);
            //}
            #endregion

            #region 团队ID
            if (!talkQueryParam.isSuper)
            {
                temp = temp.Where(u => u.OrganizeInfoID == talkQueryParam.OrganizeInfoID).AsQueryable();
            }
            #endregion

            #region 用户名称
            if (!String.IsNullOrEmpty(talkQueryParam.UserInfoShowName))
            {
                temp = temp.Where(u => u.UserInfo.UserInfoShowName.Contains(talkQueryParam.UserInfoShowName)).AsQueryable();
            }
            #endregion

            #region 团队名称
            if (!String.IsNullOrEmpty(talkQueryParam.OrganizeInfoShowName))
            {
                temp = temp.Where(u => u.OrganizeInfo.OrganizeInfoShowName.Contains(talkQueryParam.UserInfoShowName)).AsQueryable();
            }
            #endregion

            //TODO:Test
            #region 点赞数
            if (!String.IsNullOrEmpty(talkQueryParam.TalkFavorsNum))
            {
                temp = temp.Where(u => (u.TalkFavorsNum).ToString().Contains(talkQueryParam.TalkFavorsNum)).AsQueryable();
            }
            #endregion

            //TODO:极大概率出错
            #region 创建时间
            if (!String.IsNullOrEmpty(talkQueryParam.CreateTime))
            {
                temp = temp.Where(u => (u.CreateTime).ToString().Contains(talkQueryParam.CreateTime)).AsQueryable();
            }
            #endregion

            talkQueryParam.Total = temp.Count();
            return temp.OrderBy(u => u.TalkID).Skip(talkQueryParam.PageSize * (talkQueryParam.PageIndex - 1)).Take(talkQueryParam.PageSize).AsQueryable();
        }
        #endregion


        #region 排行榜
        public List<TalkTopView> GetTop(int OrdId, int TimeType, int pageSize, int pageIndex, out int total)
        {
            List<TalkTopView> list = null;
            DateTime dateTime;
            short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
            if (TimeType == 1)//月排行
            {
                list = Common.Cache.CacheHelper.GetCache("TalkMonthTop") as List<TalkTopView>;
                if (list == null)
                {
                    dateTime = DateTime.Now.AddDays(-1 * 30);
                    var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime > dateTime).AsQueryable();
                    list = (from u in Data
                            group u by u.UserInfoID into grouped
                            orderby grouped.Sum(m => m.TalkFavorsNum) descending, grouped.Key
                            select new TalkTopView { UserInfoID = (int)grouped.Key, TalkNum = (int)grouped.Sum(m => m.TalkFavorsNum) }).ToList();
                    foreach (var temp in list)
                    {
                        var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == temp.UserInfoID).FirstOrDefault();
                        temp.ShowName = t.UserInfoShowName;
                        temp.OrgId = t.OrganizeInfoID;
                        temp.Icon = t.UserInfoIcon;
                    }
                    Common.Cache.CacheHelper.SetCache("TalkMonthTop", list, DateTime.Now.AddMinutes(20));
                }

            }
            if (TimeType == 2)//季排行
            {
                list = Common.Cache.CacheHelper.GetCache("TalkSeasonTop") as List<TalkTopView>;
                if (list == null)
                {
                    dateTime = DateTime.Now.AddDays(-1 * 90);
                    var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime > dateTime).AsQueryable();
                    list = (from u in Data
                            group u by u.UserInfoID into grouped
                            orderby grouped.Sum(m => m.TalkFavorsNum) descending, grouped.Key
                            select new TalkTopView { UserInfoID = (int)grouped.Key, TalkNum = (int)grouped.Sum(m => m.TalkFavorsNum) }).ToList();
                    foreach (var temp in list)
                    {
                        var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == temp.UserInfoID).FirstOrDefault();
                        temp.ShowName = t.UserInfoShowName;
                        temp.OrgId = t.OrganizeInfoID;
                        temp.Icon = t.UserInfoIcon;

                    }
                    Common.Cache.CacheHelper.SetCache("TalkSeasonTop", list, DateTime.Now.AddMinutes(20));
                }

            }
            if (TimeType == 3)//年排行
            {
                list = Common.Cache.CacheHelper.GetCache("TalkYearTop") as List<TalkTopView>;
                if (list == null)
                {
                    dateTime = DateTime.Now.AddDays(-1 * 365);
                    var Data = CurrentDal.GetEntities(u => u.Status == delNormal && u.CreateTime > dateTime).AsQueryable();
                    list = (from u in Data
                            group u by u.UserInfoID into grouped
                            orderby grouped.Sum(m => m.TalkFavorsNum) descending, grouped.Key
                            select new TalkTopView { UserInfoID = (int)grouped.Key, TalkNum = (int)grouped.Sum(m => m.TalkFavorsNum) }).ToList();
                    foreach (var temp in list)
                    {
                        var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == temp.UserInfoID).FirstOrDefault();
                        temp.ShowName = t.UserInfoShowName;
                        temp.OrgId = t.OrganizeInfoID;
                        temp.Icon = t.UserInfoIcon;
                    }
                    Common.Cache.CacheHelper.SetCache("TalkYearTop", list, DateTime.Now.AddMinutes(20));
                }
            }
            if (TimeType == 4)//排行
            {
                list = Common.Cache.CacheHelper.GetCache("TalkAllTop") as List<TalkTopView>;
                if (list == null)
                {
                    var Data = CurrentDal.GetEntities(u => u.Status == delNormal).AsQueryable();
                    list = (from u in Data
                            group u by u.UserInfoID into grouped
                            orderby grouped.Sum(m => m.TalkFavorsNum) descending, grouped.Key
                            select new TalkTopView { UserInfoID = (int)grouped.Key, TalkNum = (int)grouped.Sum(m => m.TalkFavorsNum) }).ToList();
                    foreach (var temp in list)
                    {
                        var t = DbSession.UserInfoDal.GetEntities(u => u.UserInfoID == temp.UserInfoID).FirstOrDefault();
                        temp.ShowName = t.UserInfoShowName;
                        temp.OrgId = t.OrganizeInfoID;
                        temp.Icon = t.UserInfoIcon;
                    }
                    Common.Cache.CacheHelper.SetCache("TalkAllTop", list, DateTime.Now.AddMinutes(20));
                }
            }

            if (OrdId != -1)
            {
                list = list.Where(u => u.OrgId == OrdId).ToList();
            }
            total = list.Count();
            list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return list;
        }

        #endregion

    }
}
