using Microsoft.VisualStudio.TestTools.UnitTesting;
using OjVolunteer.BLL;
using OjVolunteer.IBLL;
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
    public class ActivityControllerTests
    {

        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        short delDelete = (short)Model.Enum.DelFlagEnum.Deleted;
        short delAuditing = (short)Model.Enum.DelFlagEnum.Auditing;
        short delUndone = (short)Model.Enum.DelFlagEnum.Undone;
        short delDoneAuditing = (short)Model.Enum.DelFlagEnum.DoneAuditing;
        public IActivityTypeService ActivityTypeService = new ActivityTypeService();
        public IActivityService ActivityService = new ActivityService();
        public IOrganizeInfoService OrganizeInfoService = new OrganizeInfoService();
        public IMajorService MajorService = new MajorService();
        public IPoliticalService PoliticalService = new PoliticalService();
        public IDepartmentService DepartmentService = new DepartmentService();

        /// <summary>
        /// 义工活动完成
        /// </summary>
        [TestMethod()]
        public void ActAccTest()
        {
            string msg = String.Empty;
            //int ActId = Convert.ToInt32(Request["aId"]);
            //int UserId = LoginUser.UserInfoID;

            int ActId = 2;
            int UserId = 2;
            Activity activity = ActivityService.GetEntities(u => u.Status == delUndone && u.ActivityManagerID == UserId&&u.ActivityID == ActId).FirstOrDefault();
            if (activity == null)
            {
                msg = "fail";
            }
            else
            {
                activity.ActivityEnd = DateTime.Now;
                activity.Status = delDoneAuditing;
                if (ActivityService.Update(activity))
                {
                    msg = "success";
                }
                else
                {
                    msg = "fail";
                }
            }
        }
    }
}
