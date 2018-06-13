using OjVolunteer.IBLL;
using OjVolunteer.Model;
using OjVolunteer.UIPortal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjVolunteer.UIPortal.Controllers
{
    public class FavorsController : BaseController
    {
        public IFavorsService FavorsService { get; set; }
        public ITalksService TalksService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        [LoginCheckFilter(BoolCheckLogin =false)]
        public ActionResult Create()
        {
            if (LoginUser == null)
            {
                return Json(new { msg = "nologin" }, JsonRequestBehavior.AllowGet);
            }
            int talkId = 0;
            if (String.IsNullOrEmpty(Request["talkId"]))
            {
                return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
            }
            talkId = int.Parse(Request["talkId"]);
            if (FavorsService.GetEntities(u => u.TalkID == talkId && u.UserInfoID == LoginUser.UserInfoID).Count() > 0)
            {
                return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
            }
            Favors favors = new Favors()
            {
                UserInfoID = LoginUser.UserInfoID,
                TalkID = talkId,
                CreateTime = DateTime.Now,
                ModfiedOn = DateTime.Now,
                Status = (short)Model.Enum.DelFlagEnum.Normal,
            };
            Talks talks = TalksService.GetEntities(u => u.TalkID == talkId).FirstOrDefault();
            talks.TalkFavorsNum = talks.TalkFavorsNum + 1;
            if (FavorsService.Add(favors) != null&& TalksService.Update(talks))
            {
                return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
        }
    }
}