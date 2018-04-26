using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model
{
    [MetadataType(typeof(DepartmentValidate))]
    public partial class Department
    {

    }

    public class DepartmentValidate
    {
        [StringLength(15, MinimumLength = 2, ErrorMessage = "学院名称长度在2到15长度之内")]
        public string DepartmentName { get; set; }
    }
}
