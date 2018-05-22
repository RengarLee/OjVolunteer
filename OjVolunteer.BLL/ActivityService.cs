using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.BLL
{
    public partial class ActivityService
    {
        public Boolean AddTime(int actId)
        {
            bool flag = false;
            short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
            var EnrollList = DbSession.UserEnrollDal.GetEntities(u => u.Status == delNormal).AsQueryable();
            //TODO 未测试
            foreach (var Enroll in EnrollList)
            {
                //添加活动详情
                ActivityDetail activityDetail = new ActivityDetail();
                activityDetail.Status = (short)Model.Enum.DelFlagEnum.Normal;
                activityDetail.UserInfoId = Enroll.UserInfoID;
                activityDetail.ActivityID = Enroll.ActivityID;
                activityDetail.CreateTime = DateTime.Now;
                activityDetail.ModfiedOn = activityDetail.CreateTime;
                TimeSpan timeSpan = (TimeSpan)(Enroll.UserEnrollActivityEnd - Enroll.UserEnrollActivityStart);
                double Time = timeSpan.Hours * 60 + timeSpan.Minutes;
                activityDetail.ActivityDetailTime = (decimal)Time;
                DbSession.ActivityDetailDal.Update(activityDetail);

                //用户总时长累积
                UserDuration userDuration = DbSession.UserDurationDal.GetEntities(u => u.UserDurationID == Enroll.UserInfoID).FirstOrDefault();
                //总时长
                userDuration.UserDurationTotal = userDuration.UserDurationTotal + (decimal)Time;
                if (userDuration.UserDurationPartyTime != null)
                    userDuration.UserDurationPartyTotal = userDuration.UserDurationPartyTotal + (decimal)Time;
                else if (userDuration.UserDurationPropartyTime != null)
                    userDuration.UserDurationPropartyTotal = userDuration.UserDurationPropartyTotal + (decimal)Time;
                else
                    userDuration.UserDurationNormalTotal = userDuration.UserDurationNormalTotal + (decimal)Time;
                DbSession.UserDurationDal.Update(userDuration);
                
            }
            DbSession.SaveChanges();
            return flag;
        }
    }
}
