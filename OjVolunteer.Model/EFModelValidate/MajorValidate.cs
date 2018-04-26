using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model
{
    [MetadataType(typeof(MajorValidate))]
    public partial class Major
    {

    }

    public class MajorValidate
    {
        [StringLength(15, MinimumLength = 2, ErrorMessage = "专业名称长度在2到15长度之内")]
        public string MajorName { get; set; }
    }
}
