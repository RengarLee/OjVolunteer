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
    
    [Serializable]
    public partial class UserBadge
    {
        public int UserBadgeID { get; set; }
        public int UserInfoID { get; set; }
        public int BadgeID { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<short> Staus { get; set; }
    
        public virtual Badge Badge { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
