 
using OjVolunteer.EFDAL;
using OjVolunteer.IDAL;

namespace OjVolunteer.DALFactory
{
    public partial class DbSession :IDbSession
    {   		
		public IactconditionDal actconditionDal
		{
			get { return StaticDalFactory.GetactconditionDal(); }
		}
		
		public IactenrollDal actenrollDal
		{
			get { return StaticDalFactory.GetactenrollDal(); }
		}
		
		public IactiveindexDal activeindexDal
		{
			get { return StaticDalFactory.GetactiveindexDal(); }
		}
		
		public IactivitytypeDal activitytypeDal
		{
			get { return StaticDalFactory.GetactivitytypeDal(); }
		}
		
		public IactsdetailDal actsdetailDal
		{
			get { return StaticDalFactory.GetactsdetailDal(); }
		}
		
		public IchangeDal changeDal
		{
			get { return StaticDalFactory.GetchangeDal(); }
		}
		
		public IdepartmentDal departmentDal
		{
			get { return StaticDalFactory.GetdepartmentDal(); }
		}
		
		public IfavorsDal favorsDal
		{
			get { return StaticDalFactory.GetfavorsDal(); }
		}
		
		public ImajorDal majorDal
		{
			get { return StaticDalFactory.GetmajorDal(); }
		}
		
		public IorganizeinfosDal organizeinfosDal
		{
			get { return StaticDalFactory.GetorganizeinfosDal(); }
		}
		
		public IpoliticalDal politicalDal
		{
			get { return StaticDalFactory.GetpoliticalDal(); }
		}
		
		public IstatusDal statusDal
		{
			get { return StaticDalFactory.GetstatusDal(); }
		}
		
		public ItalkDal talkDal
		{
			get { return StaticDalFactory.GettalkDal(); }
		}
		
		public ItimedetailDal timedetailDal
		{
			get { return StaticDalFactory.GettimedetailDal(); }
		}
		
		public IuserdurationDal userdurationDal
		{
			get { return StaticDalFactory.GetuserdurationDal(); }
		}
		
		public IuserinfoDal userinfoDal
		{
			get { return StaticDalFactory.GetuserinfoDal(); }
		}
		
		public IuserinteDal userinteDal
		{
			get { return StaticDalFactory.GetuserinteDal(); }
		}
	}
}