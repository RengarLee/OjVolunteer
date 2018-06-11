using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model.Param
{
    public class OrganizeQueryParam : BaseParam
    {
        /// <summary>
        /// 团队ID
        /// </summary>
        public string OrganizeInfoID { get; set; }
        /// <summary>
        /// 团队用户名
        /// </summary>
        public string OrganizeInfoLoginId { get; set; }
        /// <summary>
        /// 团队昵称
        /// </summary>
        public string OrganizeInfoShowName { get; set; }
        /// <summary>
        /// 团队负责人
        /// </summary>
        public string OrganizeInfoPeople { get; set; }
        /// <summary>
        /// 团队联系手机号
        /// </summary>
        public string OrganizeInfoPhone { get; set; }
        /// <summary>
        /// 团队联系邮箱
        /// </summary>
        public string OrganizeInfoEmail { get; set; }
        /// <summary>
        /// 团队最后登录时间
        /// </summary>
        public string OrganizeInfoLastTime { get; set; }
        /// <summary>
        /// 团队创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 团队状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 团队活动数
        /// </summary>
        public string ActivityCount { get; set; }

    }
}
