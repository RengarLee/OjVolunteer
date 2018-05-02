﻿using OjVolunteer.IBLL;
using OjVolunteer.Model;
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

        public ActionResult Create()
        {
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

            if (FavorsService.Add(favors) != null)
            {
                Talks talks =  TalksService.GetEntities(u => u.TalkID == talkId).FirstOrDefault();
                talks.TalkFavorsNum = talks.TalkFavorsNum + 1;
                if (TalksService.Update(talks))
                {
                    return Json(new { msg = "success",num=talks.TalkFavorsNum }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { msg = "fail" }, JsonRequestBehavior.AllowGet);
        }
    }
}