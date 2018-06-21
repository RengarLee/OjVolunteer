using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Model
{
    [MetadataType(typeof(ActivityValidate))]
    public partial class Activity
    { }

    public class ActivityValidate
    {

        /// <summary>
        /// 活动名称
        /// </summary>  
        [StringLength(30 ,MinimumLength = 5, ErrorMessage ="活动名称不少于5个字")]
        [Display(Name ="活动名称")]
        public string ActivityName{ get; set; }      
        /// <summary>
        /// 活动人数名额
        /// </summary>
        [Range(1,99, ErrorMessage = "人数名额不超过99")]
        [Required(ErrorMessage = "活动人数名额不为0")]
        [Display(Name = "活动人数名额")]
        public int ActivityPrediNum { get; set; }

        [Required(ErrorMessage ="报名开始时间不为空")]
        [Display(Name = "报名开始时间")]
        public System.DateTime ActivityEnrollStart { get; set; }

        [Required(ErrorMessage = "报名结束时间不为空")]
        [Display(Name = "报名结束时间")]
        public System.DateTime ActivityEnrollEnd { get; set; }

        [StringLength(50,ErrorMessage = "长度在2到50字符之内")]
        [Display(Name = "活动详细地址")]
        public string ActivityAddress { get; set; }

    }
}
