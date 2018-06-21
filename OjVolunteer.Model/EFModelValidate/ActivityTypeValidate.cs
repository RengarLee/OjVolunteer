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
     
        public string ActivityTypeName { get; set; }
    }
}
