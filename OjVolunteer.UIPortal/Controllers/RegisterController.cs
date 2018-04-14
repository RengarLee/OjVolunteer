
using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class RegisterController : Controller
    {
        short delNormal = (short)DelFlagEnum.Normal;
        public IOrganizeInfoService OrganizeInfoService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        public IPoliticalService PoliticalService { get; set; }
        // GET: Register
        public ActionResult Index()
        {
            var allPolitical = PoliticalService.GetEntities(u => u.Status == delNormal).ToList();
            ViewData["PoliticalList"] = (from u in allPolitical
                                         select new SelectListItem() { Selected = false, Text = u.PoliticalName, Value = u.PoliticalID + "" }).ToList();
            var allOrganizeInfo = OrganizeInfoService.GetEntities(u => u.Status == delNormal).ToList();
            ViewData["OrganizeInfoList"] = (from u in allOrganizeInfo
                                            select new SelectListItem() { Selected = false, Text = u.OrganizeInfoShowName, Value = u.OrganizeInfoID+"" }).ToList();
            return View();
        }

        #region UserRegister 个人用户注册
        public ActionResult UserRegister(string loginname, string pwd, string nickname, string phone, string OrganizeInfoList, string PoliticalList)
        {
            try
            {
                UserInfo user = new UserInfo
                {
                    UserInfoLoginId = loginname,
                    UserInfoPwd = Common.Encryption.MD5Helper.Get_MD5(pwd),
                    UserInfoShowName = nickname,
                    UserInfoPhone = phone,
                    OrganizeinfoID = Convert.ToInt32(OrganizeInfoList),
                    UpdatePoliticalID = Convert.ToInt32(PoliticalList),
                    Status = delNormal,
                    ModfiedOn = DateTime.Now,
                    CreateTime = DateTime.Now
                };
                UserInfoService.Add(user);
                return Content("ok");
            }
            catch (Exception e)
            {
                return Content("error");
            }
        }
        #endregion

        #region OrganizeRegiste 组织用户注册
        public ActionResult OrganizeRegister(string loginname, string pwd, string nickname, string people, string phone)
        {
            try
            {
                OrganizeInfo organize = new OrganizeInfo
                {
                    OrganizeInfLoginId = loginname,
                    OrganizeInfoPwd = Common.Encryption.MD5Helper.Get_MD5(pwd),
                    OrganizeInfoShowName = nickname,
                    OrganizeInfoPeople = people,
                    OrganizeInfoPhone = phone,
                    //需要通过上层组织审核才能登录
                    Status = (short)Model.Enum.DelFlagEnum.Auditing,
                    ModfiedOn = DateTime.Now,
                    CreateTime = DateTime.Now
                };
                OrganizeInfoService.Add(organize);
                return Content("ok");
            }
            catch (Exception e)
            {
                return Content("error");
            }
        }
        #endregion

        #region ValidateName 验证用户名是否重复
        public ActionResult ValidateName()
        {
            string loginId = Request["name"];
            int usertype = Convert.ToInt32(Request["usertype"]);
            if (usertype == 0)
            {
                UserInfo userInfo = UserInfoService.GetEntities(u => u.UserInfoLoginId == loginId).FirstOrDefault();
                if (userInfo == null)
                {
                    return Content("ok");
                }
            }
            if (usertype == 1)
            {
                OrganizeInfo organizeInfo = OrganizeInfoService.GetEntities(u => u.OrganizeInfLoginId == loginId).FirstOrDefault();
                if (organizeInfo == null)
                {
                    return Content("ok");
                }
            }
            return Content("error");
        }
        #endregion

    }
}