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
        /// 组织ID
        /// </summary>
        public string OrganizeInfoID { get; set; }
        /// <summary>
        /// 组织用户名
        /// </summary>
        public string OrganizeInfoLoginId { get; set; }
        /// <summary>
        /// 组织昵称
        /// </summary>
        public string OrganizeInfoShowName { get; set; }
        /// <summary>
        /// 组织负责人
        /// </summary>
        public string OrganizeInfoPeople { get; set; }
        /// <summary>
        /// 组织联系手机号
        /// </summary>
        public string OrganizeInfoPhone { get; set; }
        /// <summary>
        /// 组织联系邮箱
        /// </summary>
        public string OrganizeInfoEmail { get; set; }
        /// <summary>
        /// 组织最后登录时间
        /// </summary>
        public string OrganizeInfoLastTime { get; set; }
        /// <summary>
        /// 组织创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 组织状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 组织活动数
        /// </summary>
        public string ActivityCount { get; set; }

    }
}
