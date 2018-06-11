using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model.Param
{
    /// <summary>
    /// 团队用户查询志愿者用户所使用的参数
    /// </summary>
    public class UserQueryParam:BaseParam
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public String UserInfoID { get; set; }
        /// <summary>
        /// 用户登录ID
        /// </summary>
        public String UserInfoLoginId { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public String UserInfoStuId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public String UserInfoShowName { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public String PoliticalName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public String UserInfoPhone { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public String UserInfoEmail { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public String MajorName { get; set; }
        /// <summary>
        /// 学院名称
        /// </summary>
        public String DepartmentName { get; set; }

        /// <summary>
        /// 团队昵ID
        /// </summary>
        public int OrganizeInfoID { get; set; }
        /// <summary>
        /// 是否为最大团队
        /// </summary>
        public bool isSuper { get; set; }
        /// <summary>
        /// 团队昵称
        /// </summary>
        public String OrganizeInfoShowName { get; set; }
        /// <summary>
        /// 心得数目
        /// </summary>
        public String UserInfoTalkCount { get; set; }
        /// <summary>
        /// 普通志愿者活动时长
        /// </summary>
        public String UserDurationNormalTotal { get; set; }
        /// <summary>
        /// 预备党员志愿者活动时长
        /// </summary>
        public String UserDurationPropartyTotal { get; set; }
        /// <summary>
        /// 党员志愿者活动时长
        /// </summary>
        public String UserDurationPartyTotal { get; set; }
        /// <summary>
        /// 志愿者活动总时长
        /// </summary>
        public String UserDurationTotal { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public String UserInfoLastTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public String Status { get; set; }
    }
}
