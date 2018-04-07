﻿using System;
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
	public partial class ActivityService:BaseService<Activity>,IActivityService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.ActivityDal;
        } 
	}
	
	public partial class ActivityDetailService:BaseService<ActivityDetail>,IActivityDetailService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.ActivityDetailDal;
        } 
	}
	
	public partial class ActivityTypeService:BaseService<ActivityType>,IActivityTypeService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.ActivityTypeDal;
        } 
	}
	
	public partial class DepartmentService:BaseService<Department>,IDepartmentService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.DepartmentDal;
        } 
	}
	
	public partial class FavorsService:BaseService<Favors>,IFavorsService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.FavorsDal;
        } 
	}
	
	public partial class IntegralsService:BaseService<Integrals>,IIntegralsService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.IntegralsDal;
        } 
	}
	
	public partial class MajorService:BaseService<Major>,IMajorService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.MajorDal;
        } 
	}
	
	public partial class OrganizeInfoService:BaseService<OrganizeInfo>,IOrganizeInfoService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.OrganizeInfoDal;
        } 
	}
	
	public partial class PoliticalService:BaseService<Political>,IPoliticalService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.PoliticalDal;
        } 
	}
	
	public partial class TalksService:BaseService<Talks>,ITalksService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.TalksDal;
        } 
	}
	
	public partial class UserDurationService:BaseService<UserDuration>,IUserDurationService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserDurationDal;
        } 
	}
	
	public partial class UserEnrollService:BaseService<UserEnroll>,IUserEnrollService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserEnrollDal;
        } 
	}
	
	public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService 
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoDal;
        } 
	}
}