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
        [RegularExpression("^\\w{6,18}$", ErrorMessage = "用户名长度为6到18位只能由数字，字符，下划线组成")]
        [Required(ErrorMessage = "登录名不为空")]
        public string UserInfoLoginId { get; set; }

        /// <summary>
        /// 义工密码
        /// </summary>
        //[RegularExpression("^\\w{6,18}$", ErrorMessage = "密码长度为6到18位只能由数字，字符，下划线组成")]
        [Required(ErrorMessage = "用户密码不能为空")]
        public string UserInfoPwd { get; set; }

        ///// <summary>
        ///// 义工学号
        ///// </summary>
        ////[RegularExpression("^\\w{6,18}$", ErrorMessage = "密码长度为6到18位只能由数字，字符，下划线组成")]
        //[RegularExpression("^\\d{+}$", ErrorMessage = "学号不符合格式")]
        //public string UserInfoStuId { get; set; }

        /// <summary>
        /// 义工昵称
        /// </summary>
        [RegularExpression("^\\w{6,18}$", ErrorMessage = "用户名长度为6到18位只能由数字，字符，下划线组成")]
        [Required(ErrorMessage = "用户昵称不为空")]
        public string UserInfoShowName { get; set; }

        ///// <summary>
        ///// 义工联系人
        ///// </summary>
        //[RegularExpression("^[\u4e00-\u9fa5_a-zA-Z]{2,5}$", ErrorMessage = "联系人姓名长度2到5")]
        //[Required(ErrorMessage = "联系人不能为空")]
        //public string UserInfoPeople { get; set; }

        ///// <summary>
        ///// 联系方式
        ///// </summary>
        //[RegularExpression("^\\d{11}$", ErrorMessage = "手机号不符合格式")]
        //public string UserInfoPhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        //[RegularExpression("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage = "邮箱不符合格式")]
        //public string UserInfoEmail { get; set; }
    }

}
