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
    
    public partial class OrganizeInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrganizeInfo()
        {
            this.Activity = new HashSet<Activity>();
            this.Activity1 = new HashSet<Activity>();
            this.UserInfo = new HashSet<UserInfo>();
        }
    
        public int OrganizeInfoID { get; set; }
        public string OrganizeInfLoginId { get; set; }
        public string OrganizeInfoShowName { get; set; }
        public string OrganizeInfoPwd { get; set; }
        public string OrganizeInfoPeople { get; set; }
        public string OrganizeInfoPhone { get; set; }
        public string OrganizeInfoEmail { get; set; }
        public Nullable<int> OrganizeInfoManageId { get; set; }
        public System.DateTime OrganizeInfoLastTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> ModfiedOn { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activity1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
