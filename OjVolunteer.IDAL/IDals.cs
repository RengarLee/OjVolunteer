

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OjVolunteer.Model;

namespace OjVolunteer.IDAL
{
	public partial interface IActivityDal : IBaseDal<Activity>{}
	public partial interface IActivityDetailDal : IBaseDal<ActivityDetail>{}
	public partial interface IActivityTypeDal : IBaseDal<ActivityType>{}
	public partial interface IDepartmentDal : IBaseDal<Department>{}
	public partial interface IFavorsDal : IBaseDal<Favors>{}
	public partial interface IIntegralsDal : IBaseDal<Integrals>{}
	public partial interface IMajorDal : IBaseDal<Major>{}
	public partial interface IOrganizeInfoDal : IBaseDal<OrganizeInfo>{}
	public partial interface IPoliticalDal : IBaseDal<Political>{}
	public partial interface ITalksDal : IBaseDal<Talks>{}
	public partial interface IUserDurationDal : IBaseDal<UserDuration>{}
	public partial interface IUserEnrollDal : IBaseDal<UserEnroll>{}
	public partial interface IUserInfoDal : IBaseDal<UserInfo>{}
}