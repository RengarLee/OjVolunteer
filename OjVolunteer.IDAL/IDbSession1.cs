 
namespace OjVolunteer.IDAL
{
    public partial interface IDbSession
    {   
	 
		IactconditionDal actconditionDal { get;}
	 
		IactenrollDal actenrollDal { get;}
	 
		IactiveindexDal activeindexDal { get;}
	 
		IactivitytypeDal activitytypeDal { get;}
	 
		IactsdetailDal actsdetailDal { get;}
	 
		IchangeDal changeDal { get;}
	 
		IdepartmentDal departmentDal { get;}
	 
		IfavorsDal favorsDal { get;}
	 
		ImajorDal majorDal { get;}
	 
		IorganizeinfosDal organizeinfosDal { get;}
	 
		IpoliticalDal politicalDal { get;}
	 
		IstatusDal statusDal { get;}
	 
		ItalkDal talkDal { get;}
	 
		ItimedetailDal timedetailDal { get;}
	 
		IuserdurationDal userdurationDal { get;}
	 
		IuserinfoDal userinfoDal { get;}
	 
		IuserinteDal userinteDal { get;}
	}
}