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
            this.ApplyActivity = new HashSet<Activity>();
            this.AuditActivity = new HashSet<Activity>();
            this.Talks = new HashSet<Talks>();
            this.UserInfo = new HashSet<UserInfo>();
        }
    
        public int OrganizeInfoID { get; set; }
        public string OrganizeInfoShowName { get; set; }
        public string OrganizeInfoPwd { get; set; }
        public string OrganizeInfoPeople { get; set; }
        public string OrganizeInfoPhone { get; set; }
        public string OrganizeInfoEmail { get; set; }
        public Nullable<int> OrganizeInfoManageId { get; set; }
        public System.DateTime OrganizeInfoLastTime { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime ModfiedOn { get; set; }
        public string Remark { get; set; }
        public short Status { get; set; }
        public string OrganizeInfoIcon { get; set; }
        public string OrganizeInfoLoginId { get; set; }
        public int ActivityCount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> ApplyActivity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> AuditActivity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Talks> Talks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
