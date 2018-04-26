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
        [StringLength(15, MinimumLength = 2, ErrorMessage = "政治面貌名称长度在2到15长度之内")]
        public string PoliticalName { get; set; }
    }
}
