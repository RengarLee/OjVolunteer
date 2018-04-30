using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model.Param
{
    public class TalkQueryParam:BaseParam
    {
        public int TalkID { get; set; }
        public String UserInfoName { get; set; }
        public String OrganizeInfoName { get; set; }
        public Nullable<int> TalkFavorsNum { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }


        /// <summary>
        /// 组织ID
        /// </summary>
        public int OrganizeInfoID { get; set; }
        /// <summary>
        /// 是否为最大组织
        /// </summary>
        public bool isSuper { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public String Status { get; set; }
    }
}
