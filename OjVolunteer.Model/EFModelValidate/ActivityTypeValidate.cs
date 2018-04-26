using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model
{
    [MetadataType(typeof(ActivityTypeValidate))]
    public partial class ActivityType
    {

    }

    public class ActivityTypeValidate
    {
        [StringLength(15,MinimumLength =2,ErrorMessage ="活动名称长度在2到15长度之内")]
        public string ActivityTypeName { get; set; }
    }
}
