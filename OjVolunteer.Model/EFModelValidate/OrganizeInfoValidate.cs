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
        /// 团队登录名
        /// </summary>
        //[RegularExpression("^\\w{6,18}$",ErrorMessage ="用户名长度为6到18位只能由数字，字符，下划线组成")]
        [Required(ErrorMessage ="登录名不为空")]
        [RegularExpression("^[A-Za-z0-9]{6,12}$", ErrorMessage = "登录名不符合规范")]
        public string OrganizeInfoLoginId { get; set; }

        /// <summary>
        /// 团队名称
        /// </summary>       
        [Required(ErrorMessage = "团队名称不为空")]
        [RegularExpression("^[\u4e00-\u9fa5]{2,12}$", ErrorMessage = "团队名称不符合规范")]
        public string OrganizeInfoShowName { get; set; }

        /// <summary>
        /// 团队密码
        /// </summary>
        public string OrganizeInfoPwd { get; set; }

        /// <summary>
        /// 团队联系人
        /// </summary>

        [Required(ErrorMessage = "联系人不能为空")]
        [RegularExpression("^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "联系人不符合规范")]
        public string OrganizeInfoPeople { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [RegularExpression("^\\d{11}$", ErrorMessage = "手机号不符合格式")]
        public string OrganizeInfoPhone { get; set; }

    }
}


