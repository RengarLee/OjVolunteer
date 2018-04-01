using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OjVolunteer.DALFactory;
using OjVolunteer.EFDAL;
using OjVolunteer.IBLL;
using OjVolunteer.IDAL;
using OjVolunteer.Model;

namespace OjVolunteer.BLL
{	
	public partial class actconditionService:BaseService<actcondition>,IactconditionService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.actconditionDal;
        } 
	}
	
	public partial class actenrollService:BaseService<actenroll>,IactenrollService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.actenrollDal;
        } 
	}
	
	public partial class activeindexService:BaseService<activeindex>,IactiveindexService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.activeindexDal;
        } 
	}
	
	public partial class activitytypeService:BaseService<activitytype>,IactivitytypeService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.activitytypeDal;
        } 
	}
	
	public partial class actsdetailService:BaseService<actsdetail>,IactsdetailService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.actsdetailDal;
        } 
	}
	
	public partial class changeService:BaseService<change>,IchangeService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.changeDal;
        } 
	}
	
	public partial class departmentService:BaseService<department>,IdepartmentService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.departmentDal;
        } 
	}
	
	public partial class favorsService:BaseService<favors>,IfavorsService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.favorsDal;
        } 
	}
	
	public partial class majorService:BaseService<major>,ImajorService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.majorDal;
        } 
	}
	
	public partial class organizeinfosService:BaseService<organizeinfos>,IorganizeinfosService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.organizeinfosDal;
        } 
	}
	
	public partial class politicalService:BaseService<political>,IpoliticalService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.politicalDal;
        } 
	}
	
	public partial class statusService:BaseService<status>,IstatusService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.statusDal;
        } 
	}
	
	public partial class talkService:BaseService<talk>,ItalkService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.talkDal;
        } 
	}
	
	public partial class timedetailService:BaseService<timedetail>,ItimedetailService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.timedetailDal;
        } 
	}
	
	public partial class userdurationService:BaseService<userduration>,IuserdurationService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.userdurationDal;
        } 
	}
	
	public partial class userinfoService:BaseService<userinfo>,IuserinfoService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.userinfoDal;
        } 
	}
	
	public partial class userinteService:BaseService<userinte>,IuserinteService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.userinteDal;
        } 
	}
}