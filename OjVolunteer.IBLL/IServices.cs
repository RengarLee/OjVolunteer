 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OjVolunteer.Model;

namespace OjVolunteer.IBLL
{	
    public partial interface IActivityService:IBaseService<Activity>
    {
    }
	
    public partial interface IActivityDetailService:IBaseService<ActivityDetail>
    {
    }
	
    public partial interface IActivityTypeService:IBaseService<ActivityType>
    {
    }
	
    public partial interface IDepartmentService:IBaseService<Department>
    {
    }
	
    public partial interface IFavorsService:IBaseService<Favors>
    {
    }
	
    public partial interface IIntegralsService:IBaseService<Integrals>
    {
    }
	
    public partial interface IMajorService:IBaseService<Major>
    {
    }
	
    public partial interface IOrganizeInfoService:IBaseService<OrganizeInfo>
    {
    }
	
    public partial interface IPoliticalService:IBaseService<Political>
    {
    }
	
    public partial interface ITalksService:IBaseService<Talks>
    {
    }
	
    public partial interface IUserDurationService:IBaseService<UserDuration>
    {
    }
	
    public partial interface IUserEnrollService:IBaseService<UserEnroll>
    {
    }
	
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
    }
}