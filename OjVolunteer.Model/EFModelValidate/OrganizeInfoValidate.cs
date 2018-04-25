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

        /// <summary>
        /// 组织登录名
        /// </summary>
        [RegularExpression("^\\w{6,18}$",ErrorMessage ="用户名长度为6到18位只能由数字，字符，下划线组成")]
        [Required(ErrorMessage ="登录名不为空")]
        public string OrganizeInfoLoginId { get; set; }

        /// <summary>
        /// 组织昵称
        /// </summary>
        [RegularExpression("^\\w{6,18}$",ErrorMessage ="用户名长度为6到18位只能由数字，字符，下划线组成")]
        [Required(ErrorMessage = "用户昵称不为空")]
        public string OrganizeInfoShowName { get; set; }

        /// <summary>
        /// 组织密码
        /// </summary>
        [RegularExpression("^\\w{6,18}$", ErrorMessage = "密码长度为6到18位只能由数字，字符，下划线组成")]
        [Required(ErrorMessage = "用户密码不能为空")]
        public string OrganizeInfoPwd { get; set; }

        /// <summary>
        /// 组织联系人
        /// </summary>
        [RegularExpression("^[\u4e00-\u9fa5_a-zA-Z]{2,5}$", ErrorMessage = "联系人姓名长度2到5")]
        [Required(ErrorMessage = "联系人不能为空")]
        public string OrganizeInfoPeople { get; set; }


        /// <summary>
        /// 联系方式
        /// </summary>
        [RegularExpression("^\\d{11}$", ErrorMessage = "手机号不符合格式")]
        public string OrganizeInfoPhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        //[RegularExpression("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage = "邮箱不符合格式")]
        //public string OrganizeInfoEmail { get; set; }
    }
}
