//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OjVolunteer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActivityDetail
    {
        public int ActivityDetailID { get; set; }
        public int UserInfoId { get; set; }
        public Nullable<int> ActivityID { get; set; }
        public decimal ActivityDetailTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> ModfiedOn { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
    
        public virtual Activity Activity { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
