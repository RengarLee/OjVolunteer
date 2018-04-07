 
namespace OjVolunteer.IDAL
{
    public partial interface IDbSession
    {   
	 
		IActivityDal ActivityDal { get;}
	 
		IActivityDetailDal ActivityDetailDal { get;}
	 
		IActivityTypeDal ActivityTypeDal { get;}
	 
		IDepartmentDal DepartmentDal { get;}
	 
		IFavorsDal FavorsDal { get;}
	 
		IIntegralsDal IntegralsDal { get;}
	 
		IMajorDal MajorDal { get;}
	 
		IOrganizeInfoDal OrganizeInfoDal { get;}
	 
		IPoliticalDal PoliticalDal { get;}
	 
		ITalksDal TalksDal { get;}
	 
		IUserDurationDal UserDurationDal { get;}
	 
		IUserEnrollDal UserEnrollDal { get;}
	 
		IUserInfoDal UserInfoDal { get;}
	}
}