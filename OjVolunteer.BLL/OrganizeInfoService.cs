
using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Param;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OjVolunteer.BLL
{
    public partial class OrganizeInfoService : BaseService<OrganizeInfo>, IOrganizeInfoService
    {
        #region QrganizeQuery of Multiple conditions 
        /// <summary>
        /// 多条件查询 
        /// </summary>
        /// <param name="organizeQueryParam"></param>
        /// <param name="LoginUserId"> 自身ID</param>
        /// <returns></returns>
        public IQueryable<OrganizeInfo> LoadPageData(OrganizeQueryParam organizeQueryParam, int loginUserId)
        {
            var temp = CurrentDal.GetEntities(u => u.Status == 0 || u.Status == 1).AsQueryable();

            #region OrganizeInfoID
            if (!String.IsNullOrEmpty(organizeQueryParam.OrganizeInfoID))
            {
                temp = temp.Where(u => (u.OrganizeInfoID).ToString().Contains(organizeQueryParam.OrganizeInfoID)).AsQueryable();
            }
            #endregion

            #region LoginId
            if (!String.IsNullOrEmpty(organizeQueryParam.OrganizeInfoLoginId))
            {
                temp = temp.Where(u => u.OrganizeInfoLoginId.Contains(organizeQueryParam.OrganizeInfoLoginId)).AsQueryable();
            }
            #endregion

            #region ShowName
            if (!String.IsNullOrEmpty(organizeQueryParam.OrganizeInfoShowName))
            {
                temp = temp.Where(u => u.OrganizeInfoShowName.Contains(organizeQueryParam.OrganizeInfoShowName)).AsQueryable();
            }
            #endregion

            #region People
            if (!String.IsNullOrEmpty(organizeQueryParam.OrganizeInfoPeople))
            {
                temp = temp.Where(u => u.OrganizeInfoPeople.Contains(organizeQueryParam.OrganizeInfoPeople)).AsQueryable();
            }
            #endregion

            #region Phone
            if (!String.IsNullOrEmpty(organizeQueryParam.OrganizeInfoPhone))
            {
                temp = temp.Where(u => u.OrganizeInfoPhone.Contains(organizeQueryParam.OrganizeInfoPhone)).AsQueryable();
            }
            #endregion

            #region Email
            if (!string.IsNullOrEmpty(organizeQueryParam.OrganizeInfoEmail))
            {
                temp = temp.Where(u => u.OrganizeInfoEmail.Contains(organizeQueryParam.OrganizeInfoEmail)).AsQueryable();
            }
            #endregion

            #region LastTime
            if (!String.IsNullOrEmpty(organizeQueryParam.OrganizeInfoLastTime))
            {
                temp = temp.Where(u => (u.OrganizeInfoLastTime).ToString().Contains(organizeQueryParam.OrganizeInfoLastTime)).AsQueryable();
            }
            #endregion

            #region CreateTime
            if (!String.IsNullOrEmpty(organizeQueryParam.CreateTime))
            {
                temp = temp.Where(u => (u.CreateTime).ToString().Contains(organizeQueryParam.CreateTime)).AsQueryable();
            }
            #endregion

            #region ActivityCount
            if (!String.IsNullOrEmpty(organizeQueryParam.ActivityCount))
            {
                temp = temp.Where(u => (u.ActivityCount).ToString().Contains(organizeQueryParam.ActivityCount)).AsQueryable();
            }
            #endregion
            temp = temp.Where(u => u.OrganizeInfoID != loginUserId).AsQueryable();
            organizeQueryParam.Total = temp.Count();
            return temp.OrderBy(u=>u.OrganizeInfoID).Skip((organizeQueryParam.PageIndex - 1) * organizeQueryParam.PageSize).Take(organizeQueryParam.PageSize).AsQueryable();
        }
        #endregion

    }
}