using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model.Enum
{
    public enum DelFlagEnum
    {
        /// <summary>
        /// 正常状态
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 删除状态
        /// </summary>
        Deleted = 1,
        /// <summary>
        /// 待审核状态
        /// 用途：义工更改政治面貌后 组织账号申请
        /// </summary>
        Auditing = 2,
        /// <summary>
        /// 草稿状态
        /// 用途:心得创建却未发布或活动申请创建却未提交
        /// </summary>
        Draft = 3,
    }
}
