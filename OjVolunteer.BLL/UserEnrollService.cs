﻿using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.BLL
{
    public partial class UserEnrollService
    {

        //签到
        public bool SignIn(int aId, List<int> uIdList)
        {
            List<UserEnroll> Data = CurrentDal.GetEntities(u => u.ActivityID == aId && uIdList.Contains(u.UserInfoID)).ToList();
            foreach (var temp in Data)
            {
                temp.UserEnrollActivityStart = DateTime.Now;
                temp.Status = (short)Model.Enum.DelFlagEnum.Auditing;
            }
            return Update(Data);
        }

        //签退
        public bool SignOut(int aId, List<int> uIdList)
        {
            List<UserEnroll> Data = CurrentDal.GetEntities(u => u.ActivityID == aId && uIdList.Contains(u.UserInfoID)).ToList();
            foreach (var temp in Data)
            {
                temp.UserEnrollActivityEnd = DateTime.Now;
                TimeSpan timeSpan = (TimeSpan)(temp.UserEnrollActivityEnd - temp.UserEnrollActivityStart);
                double Time = timeSpan.Hours * 60 + timeSpan.Minutes;
                temp.ActivityTime = (decimal)Time;
                temp.Status = (short)Model.Enum.DelFlagEnum.Normal;
            }
            return Update(Data);
        }
    }
}
