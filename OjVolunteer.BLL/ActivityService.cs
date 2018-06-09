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
        //private BadgeService BadgeService = new BadgeService();
        public Boolean AddTime(int actId)
        {
            bool flag = false;
            try
            {
                short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
                short delDoneAuditing = (short)Model.Enum.DelFlagEnum.DoneAuditing;
                Activity activity = DbSession.ActivityDal.GetEntities(u => u.ActivityID == actId&&u.Status == delDoneAuditing).FirstOrDefault();
                var EnrollList = DbSession.UserEnrollDal.GetEntities(u=>u.ActivityID ==actId&&u.Status==delNormal).AsQueryable();
                foreach (var Enroll in EnrollList)
                {
                    //添加活动详情
                    ActivityDetail activityDetail = new ActivityDetail();
                    activityDetail.Status = (short)Model.Enum.DelFlagEnum.Normal;
                    activityDetail.UserInfoId = Enroll.UserInfoID;
                    activityDetail.ActivityID = Enroll.ActivityID;
                    activityDetail.CreateTime = DateTime.Now;
                    activityDetail.ModfiedOn = activityDetail.CreateTime;
                    activityDetail.ActivityDetailTime = (decimal)Enroll.ActivityTime;
                    DbSession.ActivityDetailDal.Add(activityDetail);

                    //用户总时长累积
                    UserDuration userDuration = DbSession.UserDurationDal.GetEntities(u => u.UserDurationID == Enroll.UserInfoID).FirstOrDefault();
                    //总时长
                    userDuration.UserDurationTotal = userDuration.UserDurationTotal + (decimal)Enroll.ActivityTime;
                    if (userDuration.UserDurationPartyTime != null)
                        userDuration.UserDurationPartyTotal = userDuration.UserDurationPartyTotal + (decimal)Enroll.ActivityTime;
                    else if (userDuration.UserDurationPropartyTime != null)
                    {
                        userDuration.UserDurationPropartyTotal = userDuration.UserDurationPropartyTotal + (decimal)Enroll.ActivityTime;
                        //50小时志愿者徽章
                        if (userDuration.UserDurationPropartyTotal >= 50)
                        {
                            UserBadge userBadge = new UserBadge();
                            //徽章ID
                            userBadge.BadgeID = 1;
                            userBadge.UserInfoID = userDuration.UserDurationID;
                            userBadge.CreateTime = DateTime.Now;
                            userDuration.Status = delNormal;
                            DbSession.UserBadgeDal.Add(userBadge);
                        }
                    }
                    else
                        userDuration.UserDurationNormalTotal = userDuration.UserDurationNormalTotal + (decimal)Enroll.ActivityTime;
                    DbSession.UserDurationDal.Update(userDuration);
                }
                activity.Status = delNormal;
                activity.ModfiedOn = DateTime.Now;
                DbSession.ActivityDal.Update(activity);
                DbSession.SaveChanges();
                flag = true;
            }
            catch (Exception e)
            {
                flag = false;
            }
            return flag;
        }

    }
}
