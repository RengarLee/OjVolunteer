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
    
    public partial class actenroll
    {
        public int actenroll_id { get; set; }
        public int actsdetail_id { get; set; }
        public int userinfo_id { get; set; }
        public System.DateTime actenroll_time { get; set; }
        public int status_id { get; set; }
        public string actenroll_remarks { get; set; }
    
        public virtual actsdetail actsdetail { get; set; }
        public virtual status status { get; set; }
        public virtual userinfo userinfo { get; set; }
    }
}
