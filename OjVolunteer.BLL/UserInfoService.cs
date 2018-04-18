using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.BLL
{
    public partial class UserInfoService :BaseService<UserInfo>, IUserInfoService
    {
        #region 多条件查询
        public IQueryable<UserInfo> LoadPageData(UserQueryParam userQueryParam)
        {
            var temp = DbSession.UserInfoDal.GetEntities(u => true);

            #region 状态
            short delFlag = -1;
          if (!String.IsNullOrEmpty(userQueryParam.Status))
            {
                if (("正常").Contains(userQueryParam.Status))
                {
                    delFlag = 0;
                }
                else if (("审核中").Contains(userQueryParam.Status))
                {
                    delFlag = 2;
                }
                else if (("删除").Contains(userQueryParam.Status))
                {
                    delFlag = 1;
                }
            }
            if (delFlag > -1)
            {
                temp = temp.Where(u => u.Status == delFlag);
            }
            #endregion



            //TODO:Test
            #region 用户编号
            if (!String.IsNullOrEmpty(userQueryParam.UserInfoID))
            {
                temp = temp.Where(u => (u.UserInfoID).ToString().Contains(userQueryParam.UserInfoID)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 用户登录ID
            if (!String.IsNullOrEmpty(userQueryParam.UserInfoLoginId))
            {
                temp = temp.Where(u => u.UserInfoLoginId.Contains(userQueryParam.UserInfoLoginId)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 学号
            if (!String.IsNullOrEmpty(userQueryParam.UserInfoStuId))
            {
                temp = temp.Where(u => u.UserInfoStuId.Contains(userQueryParam.UserInfoStuId)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 用户昵称
            if (!String.IsNullOrEmpty(userQueryParam.UserInfoShowName))
            {
                temp = temp.Where(u => u.UserInfoShowName.Contains(userQueryParam.UserInfoShowName)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 政治面貌
            if (!String.IsNullOrEmpty(userQueryParam.PoliticalName))
            {
                temp = temp.Where(u => u.Political.PoliticalName.Contains(userQueryParam.PoliticalName)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 手机号
            if (!String.IsNullOrEmpty(userQueryParam.UserInfoPhone))
            {
                temp = temp.Where(u => u.UserInfoPhone.Contains(userQueryParam.UserInfoPhone)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region Email
            if (!String.IsNullOrEmpty(userQueryParam.UserInfoEmail))
            {
                temp = temp.Where(u => u.UserInfoEmail.Contains(userQueryParam.UserInfoEmail)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 专业
            if (!String.IsNullOrEmpty(userQueryParam.MajorName))
            {
                temp = temp.Where(u => u.Major.MajorName.Contains(userQueryParam.MajorName)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 学院名称
            if (!String.IsNullOrEmpty(userQueryParam.DepartmentName))
            {
                temp = temp.Where(u => u.Department.DepartmentName.Contains(userQueryParam.DepartmentName)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 组织名称
            if (!String.IsNullOrEmpty(userQueryParam.OrganizeInfoShowName))
            {
                temp = temp.Where(u => u.OrganizeInfo.OrganizeInfoShowName.Contains(userQueryParam.OrganizeInfoShowName)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 心得数目
            if (!String.IsNullOrEmpty(userQueryParam.UserInfoTalkCount))
            {
                temp = temp.Where(u => (u.UserInfoTalkCount).ToString().Contains(userQueryParam.UserInfoTalkCount)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 普通义工活动时长
            if (!String.IsNullOrEmpty(userQueryParam.UserDurationNormalTotal))
            {
                temp = temp.Where(u => (u.UserDuration.UserDurationNormalTotal).ToString().Contains(userQueryParam.UserDurationNormalTotal)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 预备党员义工活动时长
            if (!String.IsNullOrEmpty(userQueryParam.UserDurationPropartyTotal))
            {
                temp = temp.Where(u => (u.UserDuration.UserDurationPropartyTotal).ToString().Contains(userQueryParam.UserDurationPropartyTotal)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 党员义工活动时长
            if (!String.IsNullOrEmpty(userQueryParam.UserDurationPartyTotal))
            {
                temp = temp.Where(u => (u.UserDuration.UserDurationPartyTotal).ToString().Contains(userQueryParam.UserDurationPartyTotal)).AsQueryable();
            }
            #endregion
            //TODO:Test
            #region 义工活动总时长
            if (!String.IsNullOrEmpty(userQueryParam.UserDurationTotal))
            {
                temp = temp.Where(u => (u.UserDuration.UserDurationTotal).ToString().Contains(userQueryParam.UserDurationTotal)).AsQueryable();
            }
            #endregion
            //TODO:极大概率出错
            #region 最后登录时间
            if (!String.IsNullOrEmpty(userQueryParam.UserInfoLastTime))
            {
                temp = temp.Where(u => (u.UserInfoLastTime).ToString().Contains(userQueryParam.UserInfoLastTime)).AsQueryable();
            }
            #endregion

            userQueryParam.Total = temp.Count();
            return temp.OrderBy(u => u.UserInfoID).Skip(userQueryParam.PageSize * (userQueryParam.PageIndex - 1)).Take(userQueryParam.PageSize).AsQueryable();
        }
        #endregion

        #region 批量更改用户政治面貌

        public int ListUpdatePolical(List<int> ids)
        {
            foreach(int id in ids)
            {
                var user = CurrentDal.GetEntities(u => u.UserInfoID == id).First();
                user.PoliticalID = user.UpdatePoliticalID;   
            }
            return NormalListByULS(ids);
        }

        #endregion
    }
}
