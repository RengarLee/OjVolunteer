 
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
		
	public partial class ActivityDal:BaseDal<Activity>,IActivityDal
    {
	}
		
	public partial class ActivityDetailDal:BaseDal<ActivityDetail>,IActivityDetailDal
    {
	}
		
	public partial class ActivityTypeDal:BaseDal<ActivityType>,IActivityTypeDal
    {
	}
		
	public partial class DepartmentDal:BaseDal<Department>,IDepartmentDal
    {
	}
		
	public partial class FavorsDal:BaseDal<Favors>,IFavorsDal
    {
	}
		
	public partial class IntegralsDal:BaseDal<Integrals>,IIntegralsDal
    {
	}
		
	public partial class MajorDal:BaseDal<Major>,IMajorDal
    {
	}
		
	public partial class OrganizeInfoDal:BaseDal<OrganizeInfo>,IOrganizeInfoDal
    {
	}
		
	public partial class PoliticalDal:BaseDal<Political>,IPoliticalDal
    {
	}
		
	public partial class TalksDal:BaseDal<Talks>,ITalksDal
    {
	}
		
	public partial class UserDurationDal:BaseDal<UserDuration>,IUserDurationDal
    {
	}
		
	public partial class UserEnrollDal:BaseDal<UserEnroll>,IUserEnrollDal
    {
	}
		
	public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
    {
	}
}