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
        [RegularExpression("^[\u4e00-\u9fa5]{2,20}$", ErrorMessage = "学院名称不符合规范")]
        public string DepartmentName { get; set; }
    }
}
