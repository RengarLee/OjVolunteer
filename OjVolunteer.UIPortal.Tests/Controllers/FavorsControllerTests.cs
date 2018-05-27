using Microsoft.VisualStudio.TestTools.UnitTesting;
using OjVolunteer.BLL;
using OjVolunteer.Model;
using OjVolunteer.UIPortal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.UIPortal.Controllers.Tests
{
    [TestClass()]
    public class FavorsControllerTests
    {
        public FavorsService FavorsService = new FavorsService();
        public TalksService TalksService = new TalksService();
        [TestMethod()]
        public void CreateTest()
        {
            String stalkId = "4";
            int talkId = 20;
            int UserInfoId = 5645;
            string msg = String.Empty;
            if (String.IsNullOrEmpty(stalkId))
            {
                msg = "fail";
            }

            if (FavorsService.GetEntities(u => u.TalkID == talkId && u.UserInfoID == UserInfoId).Count() > 0)
            {
                msg = "fail";
            }
            Favors favors = new Favors()
            {
                UserInfoID = UserInfoId,
                TalkID = talkId,
                CreateTime = DateTime.Now,
                ModfiedOn = DateTime.Now,
                Status = (short)Model.Enum.DelFlagEnum.Normal,
            };
            Talks talks = TalksService.GetEntities(u => u.TalkID == talkId).FirstOrDefault();
            talks.TalkFavorsNum = talks.TalkFavorsNum + 1;
            if (FavorsService.Add(favors) != null && TalksService.Update(talks))
            {
                msg = "success";
            }
            msg = "fail";
        }
    }
}