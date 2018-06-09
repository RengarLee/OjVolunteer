
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
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;

        #region QrganizeQuery of Multiple conditions 
        /// <summary>
        /// 多条件查询 
        /// </summary>
        /// <param name="organizeQueryParam"></param>
        /// <param name="LoginUserId"> 自身ID</param>
        /// <returns></returns>
        public IQueryable<OrganizeInfo> LoadPageData(OrganizeQueryParam organizeQueryParam, int loginUserId)
        {
            short delInvalid = (short)Model.Enum.DelFlagEnum.Invalid;

            var temp = CurrentDal.GetEntities(u=>u.Status != delInvalid).AsQueryable();

            #region 状态
            short delFlag = -1;
            if (!String.IsNullOrEmpty(organizeQueryParam.Status))
            {
                if (("正常").Contains(organizeQueryParam.Status))
                {
                    delFlag = 0;
                }
                else if (("待审核").Contains(organizeQueryParam.Status))
                {
                    delFlag = 2;
                }
                else if (("删除").Contains(organizeQueryParam.Status))
                {
                    delFlag = 1;
                }
            }
            if (delFlag > -1)
            {
                temp = temp.Where(u => u.Status == delFlag);
            }
            #endregion


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

        #region Excel导出
        public Stream ExportToExecl()
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1"); //添加一个sheet
            var orgData = CurrentDal.GetEntities(u => u.OrganizeInfoManageId!=null).AsQueryable();//获取list数据，也可以分页获取数据，以获得更高效的性能

            var _data = orgData.ToList();
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("志愿者组织ID");
            row1.CreateCell(1).SetCellValue("组织登录名");
            row1.CreateCell(2).SetCellValue("组织昵称");
            row1.CreateCell(3).SetCellValue("负责人");
            row1.CreateCell(4).SetCellValue("联系方式");
            row1.CreateCell(5).SetCellValue("邮箱");
            row1.CreateCell(6).SetCellValue("活动数目");
            row1.CreateCell(7).SetCellValue("创建时间");
            row1.CreateCell(8).SetCellValue("最后登录时间");
            row1.CreateCell(9).SetCellValue("组织状态");
            //将数据逐步写入sheet1各个行

            for (int i = 0; i < _data.Count; i++)

            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);

                rowtemp.CreateCell(0).SetCellValue(_data[i].OrganizeInfoID);

                rowtemp.CreateCell(1).SetCellValue(_data[i].OrganizeInfoLoginId);

                rowtemp.CreateCell(2).SetCellValue(_data[i].OrganizeInfoShowName);
                rowtemp.CreateCell(3).SetCellValue(_data[i].OrganizeInfoPeople);
                rowtemp.CreateCell(4).SetCellValue(_data[i].OrganizeInfoPhone);
                rowtemp.CreateCell(5).SetCellValue(string.IsNullOrEmpty(_data[i].OrganizeInfoEmail) ? "无" : _data[i].OrganizeInfoEmail);
                rowtemp.CreateCell(6).SetCellValue(_data[i].ActivityCount.ToString());
                rowtemp.CreateCell(7).SetCellValue(_data[i].CreateTime.ToString());
                rowtemp.CreateCell(8).SetCellValue(_data[i].OrganizeInfoLastTime.ToString());
                rowtemp.CreateCell(9).SetCellValue(_data[i].Status == 0 ? "正常" : _data[i].Status == 1 ? "删除" :  _data[i].Status == 2 ?"审核中": "无效");
            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            book.Write(ms);

            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }
        #endregion

        #region 组织添加
        public bool AddOrg(OrganizeInfo organizeInfo)
        {
            bool flag = false;

            String pwd = "000000";
            organizeInfo.OrganizeInfoPwd = Common.Encryption.MD5Helper.Get_MD5(pwd);
            organizeInfo.ActivityCount = 0;
            organizeInfo.CreateTime = DateTime.Now;
            organizeInfo.ModfiedOn = organizeInfo.CreateTime;
            organizeInfo.OrganizeInfoLastTime = organizeInfo.CreateTime;
            organizeInfo.Status = delNormal;
            DbSession.OrganizeInfoDal.Add(organizeInfo);
            if (DbSession.SaveChanges() > 0)
                flag = true;
            return flag;
        }
        #endregion
    }
}