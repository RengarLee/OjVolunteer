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
        public int UserInfoId { get; set; }
        public DateTime CreateTime { get; set; }
        public String ShowName { get; set; }
        public String Icon { get; set; }
        public String TalkContent { get; set; }
        public bool Favors { get; set; }
        public List<string> ImagePath { get; set; }
    }
}