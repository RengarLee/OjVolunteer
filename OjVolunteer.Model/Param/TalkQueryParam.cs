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
        public String UserInfoShowName { get; set; }
        public String OrganizeInfoShowName { get; set; }
        public String TalkFavorsNum { get; set; }
        public String CreateTime { get; set; }
        /// <summary>
        /// 团队ID
        /// </summary>
        public int OrganizeInfoID { get; set; }
        /// <summary>
        /// 是否为最大团队
        /// </summary>
        public bool isSuper { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public String Status { get; set; }
    }
}
