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
        /// 用途:由正常状态被删除 
        /// </summary>
        Deleted = 1,
        /// <summary>
        /// 待审核状态
        /// 用途：义工更改政治面貌后 组织账号申请   活动签到后
        /// </summary>
        Auditing = 2,
        /// <summary>
        /// 无效状态
        /// 用途:申请未通过 或 活动报名但未签到
        /// </summary>
        Invalid = 3,
        /// <summary>
        /// 未完成状态
        /// 用途：活动通过审批但未完成 
        /// </summary>
        Undone = 4,
        /// <summary>
        /// 完成待审核状态
        /// 用途：活动完成但未经过审核
        /// </summary>
        DoneAuditing = 5
    }
}
