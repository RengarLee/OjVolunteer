 
using OjVolunteer.EFDAL;
using OjVolunteer.IDAL;

namespace OjVolunteer.DALFactory
{
    public partial class DbSession :IDbSession
    {   		
		public IActivityDal ActivityDal
		{
			get { return StaticDalFactory.GetActivityDal(); }
		}
		
		public IActivityDetailDal ActivityDetailDal
		{
			get { return StaticDalFactory.GetActivityDetailDal(); }
		}
		
		public IActivityTypeDal ActivityTypeDal
		{
			get { return StaticDalFactory.GetActivityTypeDal(); }
		}
		
		public IDepartmentDal DepartmentDal
		{
			get { return StaticDalFactory.GetDepartmentDal(); }
		}
		
		public IFavorsDal FavorsDal
		{
			get { return StaticDalFactory.GetFavorsDal(); }
		}
		
		public IIntegralsDal IntegralsDal
		{
			get { return StaticDalFactory.GetIntegralsDal(); }
		}
		
		public IMajorDal MajorDal
		{
			get { return StaticDalFactory.GetMajorDal(); }
		}
		
		public IOrganizeInfoDal OrganizeInfoDal
		{
			get { return StaticDalFactory.GetOrganizeInfoDal(); }
		}
		
		public IPoliticalDal PoliticalDal
		{
			get { return StaticDalFactory.GetPoliticalDal(); }
		}
		
		public ITalksDal TalksDal
		{
			get { return StaticDalFactory.GetTalksDal(); }
		}
		
		public IUserDurationDal UserDurationDal
		{
			get { return StaticDalFactory.GetUserDurationDal(); }
		}
		
		public IUserEnrollDal UserEnrollDal
		{
			get { return StaticDalFactory.GetUserEnrollDal(); }
		}
		
		public IUserInfoDal UserInfoDal
		{
			get { return StaticDalFactory.GetUserInfoDal(); }
		}
	}
}