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
        [RegularExpression("^[\u4e00-\u9fa5]{2,20}$", ErrorMessage = "专业名称不符合规范")]
        public string MajorName { get; set; }
    }
}
