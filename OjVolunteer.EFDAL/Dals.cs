 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OjVolunteer.IDAL;
using OjVolunteer.Model;

namespace OjVolunteer.EFDAL
{ 
		
	public partial class actconditionDal:BaseDal<actcondition>,IactconditionDal
    {
	}
		
	public partial class actenrollDal:BaseDal<actenroll>,IactenrollDal
    {
	}
		
	public partial class activeindexDal:BaseDal<activeindex>,IactiveindexDal
    {
	}
		
	public partial class activitytypeDal:BaseDal<activitytype>,IactivitytypeDal
    {
	}
		
	public partial class actsdetailDal:BaseDal<actsdetail>,IactsdetailDal
    {
	}
		
	public partial class changeDal:BaseDal<change>,IchangeDal
    {
	}
		
	public partial class departmentDal:BaseDal<department>,IdepartmentDal
    {
	}
		
	public partial class favorsDal:BaseDal<favors>,IfavorsDal
    {
	}
		
	public partial class majorDal:BaseDal<major>,ImajorDal
    {
	}
		
	public partial class organizeinfosDal:BaseDal<organizeinfos>,IorganizeinfosDal
    {
	}
		
	public partial class politicalDal:BaseDal<political>,IpoliticalDal
    {
	}
		
	public partial class statusDal:BaseDal<status>,IstatusDal
    {
	}
		
	public partial class talkDal:BaseDal<talk>,ItalkDal
    {
	}
		
	public partial class timedetailDal:BaseDal<timedetail>,ItimedetailDal
    {
	}
		
	public partial class userdurationDal:BaseDal<userduration>,IuserdurationDal
    {
	}
		
	public partial class userinfoDal:BaseDal<userinfo>,IuserinfoDal
    {
	}
		
	public partial class userinteDal:BaseDal<userinte>,IuserinteDal
    {
	}
}