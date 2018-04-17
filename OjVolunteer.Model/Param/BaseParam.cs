using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model.Param
{
    public class BaseParam
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Total { get; set; }
    }
}
