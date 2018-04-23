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
        //TODO:
        public int OrganizeInfoID { get; set; }

        [RegularExpression("^\\w{6,18}$",ErrorMessage ="用户名长度为6到18位只能由数字，字符，下划线组成")]
        [Required(ErrorMessage = "用户昵称不能为空")]
        public string OrganizeInfoShowName { get; set; }

        [RegularExpression("^\\w{6,18}$", ErrorMessage = "密码长度为6到18位只能由数字，字符，下划线组成")]
        [Required(ErrorMessage = "用户不能为空")]
        public string OrganizeInfoPwd { get; set; }

        [RegularExpression("^[\u4e00-\u9fa5_a-zA-Z]{2,5}$", ErrorMessage = "联系人长度2到5")]
        [Required(ErrorMessage = "联系人不能为空")]
        public string OrganizeInfoPeople { get; set; }

        [RegularExpression("^\\d{11}$", ErrorMessage = "手机号不符合格式")]
        public string OrganizeInfoPhone { get; set; }

        [RegularExpression("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage = "手机号不符合格式")]
        public string OrganizeInfoEmail { get; set; }

        public string Remark { get; set; }
        public short Status { get; set; }
    }
}
