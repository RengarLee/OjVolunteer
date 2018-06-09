using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Param;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.BLL
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        #region Excel导出
        public Stream ExportToExecl(bool isSuper, int orgId)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1"); //添加一个sheet
            var userData = CurrentDal.GetEntities(u => true).AsQueryable();//获取list数据，也可以分页获取数据，以获得更高效的性能

            //判断是否为最高权限用户
            if (!isSuper)//不是
            {
                userData = userData.Where(u => u.OrganizeInfoID == orgId).AsQueryable();
            }
            var _data = userData.ToList();
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("义工用户ID");
            row1.CreateCell(1).SetCellValue("义工用户名");
            row1.CreateCell(2).SetCellValue("义工昵称");
            row1.CreateCell(3).SetCellValue("学号");
            row1.CreateCell(4).SetCellValue("手机");
            row1.CreateCell(5).SetCellValue("邮箱");
            row1.CreateCell(6).SetCellValue("专业");
            row1.CreateCell(6).SetCellValue("学院");
            row1.CreateCell(7).SetCellValue("政治面貌");
            row1.CreateCell(8).SetCellValue("组织名称");
            row1.CreateCell(9).SetCellValue("发表心得数目");
            row1.CreateCell(10).SetCellValue("创建时间");
            row1.CreateCell(11).SetCellValue("最后登录时间");
            row1.CreateCell(12).SetCellValue("义工用户状态");
            //将数据逐步写入sheet1各个行

            for (int i = 0; i < _data.Count; i++)

            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);

                rowtemp.CreateCell(0).SetCellValue(_data[i].UserInfoID);

                rowtemp.CreateCell(1).SetCellValue(_data[i].UserInfoLoginId);

                rowtemp.CreateCell(2).SetCellValue(_data[i].UserInfoShowName);
                rowtemp.CreateCell(3).SetCellValue(string.IsNullOrEmpty(_data[i].UserInfoStuId) ? "无" : _data[i].UserInfoStuId);
                rowtemp.CreateCell(4).SetCellValue(string.IsNullOrEmpty(_data[i].UserInfoPhone) ? "无" : _data[i].UserInfoPhone);
                rowtemp.CreateCell(5).SetCellValue(string.IsNullOrEmpty(_data[i].UserInfoEmail) ? "无" : _data[i].UserInfoEmail);
                rowtemp.CreateCell(6).SetCellValue(_data[i].MajorID == null ? "无" : _data[i].Major.MajorName);
                rowtemp.CreateCell(7).SetCellValue(_data[i].Department == null ? "无" : _data[i].Department.DepartmentName);
                rowtemp.CreateCell(8).SetCellValue(_data[i].OrganizeInfo == null ? "无" : _data[i].OrganizeInfo.OrganizeInfoShowName);
                rowtemp.CreateCell(9).SetCellValue(_data[i].UserInfoTalkCount.ToString());
                rowtemp.CreateCell(10).SetCellValue(_data[i].CreateTime.ToString());
                rowtemp.CreateCell(11).SetCellValue(_data[i].UserInfoLastTime.ToString());
                rowtemp.CreateCell(12).SetCellValue(_data[i].Status == 0 ? "正常" : _data[i].Status == 1 ? "删除" : "审核中");
            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            book.Write(ms);

            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }


        #endregion

        #region 多条件查询
        public IQueryable<UserInfo> LoadPageData(UserQueryParam userQueryParam)
        {
            short delInvalid = (short)Model.Enum.DelFlagEnum.Invalid;
            var temp = DbSession.UserInfoDal.GetEntities(u => u.Status != delInvalid).AsQueryable();

            #region 状态
            short delFlag = -1;
            if (!String.IsNullOrEmpty(userQueryParam.Status))
            {
                if (("正常").Contains(userQueryParam.Status))
                {
                    delFlag = 0;
                }
                else if (("待审核").Contains(userQueryParam.Status))
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

            #region 组织ID
            if (!userQueryParam.isSuper)
            {
                temp = temp.Where(u => u.OrganizeInfoID == userQueryParam.OrganizeInfoID).AsQueryable();
            }
            #endregion

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

        #region 查找活动负责人
        public List<UserInfo> SearchUser(String key)
        {
            //查找用户 仅查找状态正常和状态审核的用户
            var list = DbSession.UserInfoDal.GetPageEntities(20, 1, out int total, u => u.UserInfoShowName.Contains(key) && (u.Status == delNormal || u.Status == delAuditing), t => t.UserInfoShowName, true).ToList(); ;
            return list;
        }
        #endregion

        #region 更改用户政治面貌

        public bool ListUpdatePolical(List<int> ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    var user = CurrentDal.GetEntities(u => u.UserInfoID == id).First();
                    if (user.UpdatePoliticalID != null)
                    {
                        user.PoliticalID = (int)user.UpdatePoliticalID;
                    }
                }
                return NormalListByULS(ids);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region 用户添加
        public bool AddUser(UserInfo userInfo)
        {
            bool flag = false;
            //用户添加
            userInfo.UserInfoTalkCount = 0;
            userInfo.CreateTime = DateTime.Now;
            userInfo.ModfiedOn = userInfo.CreateTime;
            userInfo.UserInfoLastTime = userInfo.CreateTime;
            DbSession.UserInfoDal.Add(userInfo);
            if (DbSession.SaveChanges() > 0)
                flag = true;

            //用户时长添加
            UserDuration userDuration = new UserDuration();
            userDuration.UserDurationID = userInfo.UserInfoID;
            userDuration.CreateTime = DateTime.Now;
            userDuration.ModfiedOn = userDuration.CreateTime;
            userDuration.Status = delNormal;
            userDuration.UserDurationTotal = 0;
            userDuration.UserDurationPartyTotal = 0;
            userDuration.UserDurationPropartyTotal = 0;
            userDuration.UserDurationNormalTotal = 0;
            DbSession.UserDurationDal.Add(userDuration);
            if (DbSession.SaveChanges() > 0)
                flag = true;
            return flag;

        }
        #endregion

        #region 更改密码
        public String UpdatePassWord(UserInfo user, string oldPwd, string newPwd)
        {
            //密码匹配
            if (user.UserInfoPwd.Equals(Common.Encryption.MD5Helper.Get_MD5(oldPwd)))
            {
                //密码更改
                user.UserInfoPwd = Common.Encryption.MD5Helper.Get_MD5(newPwd);
                user.ModfiedOn = DateTime.Now;
                CurrentDal.Update(user);
                if (DbSession.SaveChanges()>0)
                {
                    //更改成功
                    return "success";
                }
                //更改失败
                return "fail";
            }
            //旧密码不正确
            return "error";
        } 
        #endregion
    }
}
