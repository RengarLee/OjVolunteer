﻿using OjVolunteer.IBLL;
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

            #region 组织ID
            if (!talkQueryParam.isSuper)
            {
                temp = temp.Where(u => u.OrganizeInfoID == talkQueryParam.OrganizeInfoID).AsQueryable();
            }
            #endregion

            #region 用户昵称
            if (!String.IsNullOrEmpty(talkQueryParam.UserInfoShowName))
            {
                temp = temp.Where(u => u.UserInfo.UserInfoShowName.Contains(talkQueryParam.UserInfoShowName)).AsQueryable();
            }
            #endregion

            #region 组织昵称
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
   
    }
}
