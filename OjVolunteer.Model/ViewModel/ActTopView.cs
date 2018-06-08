using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model.ViewModel
{
    [Serializable]
    public class ActTopView
    {
        public int UserInfoID { get; set; }
        public string ShowName { get; set; }
        public decimal ActivityTime { get; set; }
        public int OrgId { get; set; }
        public string Icon { get; set; }
    }
}
