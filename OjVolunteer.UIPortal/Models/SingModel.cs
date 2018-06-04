using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OjVolunteer.UIPortal.Models
{
    public class SingModel
    {
        public int UserInfoId { get; set; }
        public string ShowName { get; set; }
        public string LoginId { get; set; }
        public DateTime? SingTime { get; set; }
        public bool isSing{get;set;}
    }
}