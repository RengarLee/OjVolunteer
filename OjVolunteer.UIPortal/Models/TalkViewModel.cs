using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OjVolunteer.UIPortal.Models
{
    public class TalkViewModel
    {
        public int TalkID { get; set; }
        public int TalkFavorsNum { get; set; }
        public DateTime CreateTime { get; set; }
        public String OrganizeInfoShowName { get; set; }
        public String OrganizeInfoIcon { get; set; }
        public String UserInfoShowName { get; set; }
        public String UserInfoIcon { get; set; }
        public String TalkContent { get; set; }
        public List<string> ImagePath { get; set; }
    }
}