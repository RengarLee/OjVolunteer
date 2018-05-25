using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model
{
    [MetadataType(typeof(PoliticalValidate))]
    public partial class Political
    {

    }

    public class PoliticalValidate
    {
        [RegularExpression("^[\u4e00-\u9fa5]{2,20}$", ErrorMessage = "政治面貌名称不符合规范")]
        public string PoliticalName { get; set; }
    }
}
