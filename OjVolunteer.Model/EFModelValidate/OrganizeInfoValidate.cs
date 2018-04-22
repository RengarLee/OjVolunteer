using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model
{
    [MetadataType(typeof(OrganizeInfoValidate))]
    public partial class OrganizeInfo
    { }

    public class OrganizeInfoValidate
    {

        [Required(ErrorMessage ="用户昵称不能为空")]
        public string OrganizeInfoShowName { get; set; }
    }
}
