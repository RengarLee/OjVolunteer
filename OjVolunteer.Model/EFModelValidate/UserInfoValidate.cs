using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model
{
    [MetadataType(typeof(UserInfoValidate))]
    public partial class UserInfo
    { }

    public class UserInfoValidate
    {

        /// <summary>
        /// 义工登录名
        /// </summary>
        [Required(ErrorMessage = "登录名不为空")]
        [RegularExpression("^[A-Za-z0-9]{6,12}$", ErrorMessage = "登录名不符合规范")]
        public string UserInfoLoginId { get; set; }

        /// <summary>
        /// 义工密码
        /// </summary>
        public string UserInfoPwd { get; set; }

        /// <summary>
        /// 义工昵称
        /// </summary>
        [Required(ErrorMessage = "用户名不为空")]
        [RegularExpression("^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "组织名称不符合规范")]
        public string UserInfoShowName { get; set; }

        [RegularExpression("^\\d{11}$", ErrorMessage = "手机号不符合格式")]
        public string UserInfoPhone { get; set; }
    }

}
