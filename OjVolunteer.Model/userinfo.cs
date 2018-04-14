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
    
    public partial class UserInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserInfo()
        {
            this.ApplyActivity = new HashSet<Activity>();
            this.ManagerActivity = new HashSet<Activity>();
            this.ActivityDetail = new HashSet<ActivityDetail>();
            this.Favors = new HashSet<Favors>();
            this.Integrals = new HashSet<Integrals>();
            this.Talks = new HashSet<Talks>();
            this.UserDuration = new HashSet<UserDuration>();
            this.UserEnroll = new HashSet<UserEnroll>();
        }
    
        public int UserInfoID { get; set; }
        public string UserInfoLoginId { get; set; }
        public string UserInfoPwd { get; set; }
        public string UserInfoStuId { get; set; }
        public string UserInfoShowName { get; set; }
        public string UserInfoPhone { get; set; }
        public string UserInfoEmail { get; set; }
        public Nullable<int> MajorID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> PoliticalID { get; set; }
        public Nullable<int> OrganizeinfoID { get; set; }
        public Nullable<System.DateTime> UserInfoLastTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> ModfiedOn { get; set; }
        public string Remark { get; set; }
        public short Status { get; set; }
        public Nullable<int> UpdatePoliticalID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> ApplyActivity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> ManagerActivity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityDetail> ActivityDetail { get; set; }
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favors> Favors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Integrals> Integrals { get; set; }
        public virtual Major Major { get; set; }
        public virtual OrganizeInfo OrganizeInfo { get; set; }
        public virtual Political Political { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Talks> Talks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDuration> UserDuration { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserEnroll> UserEnroll { get; set; }
    }
}
