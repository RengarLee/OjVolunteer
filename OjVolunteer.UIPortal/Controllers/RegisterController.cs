
using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Enum;
using OjVolunteer.UIPortal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    [LoginCheckFilter(BoolCheckLogin = false)]
    public class RegisterController : Controller
    {
        short delNormal = (short)DelFlagEnum.Normal;
        short delAuditing = (short)DelFlagEnum.Auditing;
        public IOrganizeInfoService OrganizeInfoService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        public IUserDurationService UserDurationService { get; set; }
        public IPoliticalService PoliticalService { get; set; }
        // GET: Register
        public ActionResult Index()
        {
            var allPolitical = PoliticalService.GetEntities(u => u.Status == delNormal).ToList();
            ViewData["PoliticalList"] = (from u in allPolitical
                                         select new SelectListItem() { Selected = false, Text = u.PoliticalName, Value = u.PoliticalID + "" }).ToList();
            var allOrganizeInfo = OrganizeInfoService.GetEntities(u => u.Status == delNormal && u.OrganizeInfoManageId != null).ToList();
            ViewData["OrganizeInfoList"] = (from u in allOrganizeInfo
                                            select new SelectListItem() { Selected = false, Text = u.OrganizeInfoShowName, Value = u.OrganizeInfoID+"" }).ToList();
            return View();
        }

        #region UserRegister 个人用户注册
        public ActionResult UserRegister(string loginname, string pwd, string nickname, string phone, string OrganizeInfoList, string PoliticalList)
        {
            if (!ValidateName(loginname))
            {
                return Content("exist");
            }
            Regex regex = new Regex(@"^[A-Za-z0-9]{6,12}$");
            Regex regex1 = new Regex(@"^[0-9]{6,12}$");
            if (!regex.IsMatch(loginname)||regex1.IsMatch(loginname))
            {
                return Content("fail");
            }
            //密码
            regex = new Regex(@"^[A-Za-z0-9]{6,12}$");
            if (!regex.IsMatch(pwd))
            {
                return Content("fail");
            }
            //昵称
            regex = new Regex(@"^[\u4e00-\u9fa5]{2,15}$");
            if (!regex.IsMatch(nickname))
            {
                return Content("fail");
            }
            //电话号码
            regex = new Regex(@"^\d{11}$");
            if (!regex.IsMatch(phone))
            {
                return Content("fail");
            }
            UserInfo userInfo = new UserInfo
            {
                UserInfoLoginId = loginname,
                UserInfoPwd = Common.Encryption.MD5Helper.Get_MD5(pwd),
                UserInfoShowName = nickname,
                UserInfoPhone = phone,
                OrganizeInfoID = Convert.ToInt32(OrganizeInfoList),
                UpdatePoliticalID = Convert.ToInt32(PoliticalList),
                PoliticalID = Convert.ToInt32(PoliticalList),
                Status = delAuditing,
                UserInfoIcon = System.Configuration.ConfigurationManager.AppSettings["DefaultIconPath"],
            };
            if (UserInfoService.AddUser(userInfo))
            {
                return Content("user");
            }
            else
            {
                return Content("fail");
            }
        }
        #endregion

        #region OrganizeRegiste 团队用户注册

        public ActionResult OrganizeRegister(string loginname, string pwd, string nickname, string people, string phone)
        {
            if (!ValidateName(loginname))
            {
                return Content("exist");
            }
            //验证
            //登录名
            Regex regex = new Regex(@"^[A-Za-z0-9]{6,12}$");
            Regex regex1 = new Regex(@"^[0-9]{6,12}$");
            if (!regex.IsMatch(loginname)|| regex1.IsMatch(loginname))
            {
                return Content("fail");
            }
            //密码
            regex = new Regex(@"^[A-Za-z0-9]{6,12}$");
            if (!regex.IsMatch(pwd))
            {
                return Content("fail");
            }
            //昵称
            regex = new Regex(@"^[\u4e00-\u9fa5]{2,15}$");
            if (!regex.IsMatch(nickname))
            {
                return Content("fail");
            }

            regex = new Regex(@"^[\u4e00-\u9fa5]{2,10}$");
            if (!regex.IsMatch(people))
            {
                return Content("fail");
            }
            //电话号码
            regex = new Regex(@"^\d{11}$");
            if (!regex.IsMatch(phone))
            {
                return Content("fail");
            }

            OrganizeInfo organize = new OrganizeInfo
            {
                OrganizeInfoLoginId = loginname,
                OrganizeInfoPwd = Common.Encryption.MD5Helper.Get_MD5(pwd),
                OrganizeInfoShowName = nickname,
                OrganizeInfoPeople = people,
                OrganizeInfoPhone = phone,
                Status = (short)Model.Enum.DelFlagEnum.Auditing,
                ModfiedOn = DateTime.Now,
                CreateTime = DateTime.Now,
                OrganizeInfoLastTime = DateTime.Now,
                OrganizeInfoManageId = OrganizeInfoService.GetEntities(u => u.OrganizeInfoManageId == null).FirstOrDefault().OrganizeInfoID,
                OrganizeInfoIcon = System.Configuration.ConfigurationManager.AppSettings["DefaultIconPath"],
                ActivityCount = 0
            };
            if(OrganizeInfoService.Add(organize)!=null)
                return Content("organize");
            else
                return Content("fail");
        }
        #endregion

        #region ValidateName 验证用户名是否重复
        public Boolean ValidateName( string loginId)
        {
            bool flag = false;
            UserInfo userInfo = UserInfoService.GetEntities(u => u.UserInfoLoginId == loginId).FirstOrDefault();
            if (userInfo == null)
            {
                OrganizeInfo organizeInfo = OrganizeInfoService.GetEntities(u => u.OrganizeInfoLoginId == loginId).FirstOrDefault();
                if (organizeInfo == null)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public ActionResult ValName()
        {
            string loginId = Request["name"];
            UserInfo userInfo = UserInfoService.GetEntities(u => u.UserInfoLoginId == loginId).FirstOrDefault();
            if (userInfo == null)
            {
                OrganizeInfo organizeInfo = OrganizeInfoService.GetEntities(u => u.OrganizeInfoLoginId == loginId).FirstOrDefault();
                if (organizeInfo == null)
                {
                    return Content("success");
                }
            }
            return Content("fail");
        }
        #endregion

    }
}