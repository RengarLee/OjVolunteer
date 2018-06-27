 
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
		
		public IBadgeDal BadgeDal
		{
			get { return StaticDalFactory.GetBadgeDal(); }
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
		
		public IUserBadgeDal UserBadgeDal
		{
			get { return StaticDalFactory.GetUserBadgeDal(); }
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
		
		public Iv_User_ActDetailDal v_User_ActDetailDal
		{
			get { return StaticDalFactory.Getv_User_ActDetailDal(); }
		}
	}
}