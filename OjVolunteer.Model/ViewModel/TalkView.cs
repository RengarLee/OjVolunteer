using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model.ViewModel
{
    public class TalkView
    {
        public int TalkID { get; set; }
        public int TalkFavorsNum { get; set; }
        public DateTime CreateTime { get; set; }
        public String ShowName { get; set; }
        public String Icon { get; set; }
        public String TalkContent { get; set; }
        public List<string> ImagePath { get; set; }
    }
}
