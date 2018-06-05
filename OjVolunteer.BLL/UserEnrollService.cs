using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.BLL
{
    public partial class UserEnrollService
    {

        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        short delInvalid = (short)Model.Enum.DelFlagEnum.Invalid;
        //签到
        public bool SignIn(int aId, List<int> uIdList)
        {
            List<UserEnroll> Data = CurrentDal.GetEntities(u => u.ActivityID == aId && uIdList.Contains(u.UserInfoID)&&u.Status == delInvalid).ToList();
            foreach (var temp in Data)
            {
                temp.UserEnrollActivityStart = DateTime.Now;
                temp.Status = delAuditing;
            }
            return Update(Data);
        }

        //签退
        public bool SignOut(int aId, List<int> uIdList)
        {
            List<UserEnroll> Data = CurrentDal.GetEntities(u => u.ActivityID == aId && uIdList.Contains(u.UserInfoID)&&u.Status == delAuditing).ToList();
            foreach (var temp in Data)
            {
                temp.UserEnrollActivityEnd = DateTime.Now;
                TimeSpan timeSpan = (TimeSpan)(temp.UserEnrollActivityEnd - temp.UserEnrollActivityStart);
                double Time = timeSpan.Hours + (double)timeSpan.Minutes/60;
                temp.ActivityTime = (decimal)Time;
                temp.Status = (short)Model.Enum.DelFlagEnum.Normal;
            }
            return Update(Data);
        }
    }
}
