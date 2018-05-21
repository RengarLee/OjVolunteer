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
        //签到
        public bool SignIn(int aId, List<int> uIdList)
        {
            List<UserEnroll> Data = CurrentDal.GetEntities(u => u.ActivityID == aId && uIdList.Contains(u.UserInfoID)).ToList();
            foreach (var temp in Data)
            {
                temp.UserEnrollActivityStart = DateTime.Now;
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
            }
            return Update(Data);
        }
    }
}
