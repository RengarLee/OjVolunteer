

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OjVolunteer.Model;

namespace OjVolunteer.IDAL
{
	public partial interface IactconditionDal : IBaseDal<actcondition>{}
	public partial interface IactenrollDal : IBaseDal<actenroll>{}
	public partial interface IactiveindexDal : IBaseDal<activeindex>{}
	public partial interface IactivitytypeDal : IBaseDal<activitytype>{}
	public partial interface IactsdetailDal : IBaseDal<actsdetail>{}
	public partial interface IchangeDal : IBaseDal<change>{}
	public partial interface IdepartmentDal : IBaseDal<department>{}
	public partial interface IfavorsDal : IBaseDal<favors>{}
	public partial interface ImajorDal : IBaseDal<major>{}
	public partial interface IorganizeinfosDal : IBaseDal<organizeinfos>{}
	public partial interface IpoliticalDal : IBaseDal<political>{}
	public partial interface IstatusDal : IBaseDal<status>{}
	public partial interface ItalkDal : IBaseDal<talk>{}
	public partial interface ItimedetailDal : IBaseDal<timedetail>{}
	public partial interface IuserdurationDal : IBaseDal<userduration>{}
	public partial interface IuserinfoDal : IBaseDal<userinfo>{}
	public partial interface IuserinteDal : IBaseDal<userinte>{}
}