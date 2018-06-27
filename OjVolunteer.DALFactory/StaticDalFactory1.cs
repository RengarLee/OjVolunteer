 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OjVolunteer.EFDAL;
using OjVolunteer.IDAL;

namespace OjVolunteer.DALFactory
{
    /// <summary>
    /// 由简单工厂转变成了抽象工厂。
    /// 抽象工厂  VS  简单工厂
    /// 
    /// </summary>
    public partial class StaticDalFactory
    {
   
	
		public static IActivityDal GetActivityDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ActivityDal")
				as IActivityDal;
		}
	
		public static IActivityDetailDal GetActivityDetailDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ActivityDetailDal")
				as IActivityDetailDal;
		}
	
		public static IActivityTypeDal GetActivityTypeDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ActivityTypeDal")
				as IActivityTypeDal;
		}
	
		public static IBadgeDal GetBadgeDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".BadgeDal")
				as IBadgeDal;
		}
	
		public static IDepartmentDal GetDepartmentDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".DepartmentDal")
				as IDepartmentDal;
		}
	
		public static IFavorsDal GetFavorsDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".FavorsDal")
				as IFavorsDal;
		}
	
		public static IIntegralsDal GetIntegralsDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".IntegralsDal")
				as IIntegralsDal;
		}
	
		public static IMajorDal GetMajorDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".MajorDal")
				as IMajorDal;
		}
	
		public static IOrganizeInfoDal GetOrganizeInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrganizeInfoDal")
				as IOrganizeInfoDal;
		}
	
		public static IPoliticalDal GetPoliticalDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".PoliticalDal")
				as IPoliticalDal;
		}
	
		public static ITalksDal GetTalksDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".TalksDal")
				as ITalksDal;
		}
	
		public static IUserBadgeDal GetUserBadgeDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserBadgeDal")
				as IUserBadgeDal;
		}
	
		public static IUserDurationDal GetUserDurationDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserDurationDal")
				as IUserDurationDal;
		}
	
		public static IUserEnrollDal GetUserEnrollDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserEnrollDal")
				as IUserEnrollDal;
		}
	
		public static IUserInfoDal GetUserInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoDal")
				as IUserInfoDal;
		}
	
		public static Iv_User_ActDetailDal Getv_User_ActDetailDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".v_User_ActDetailDal")
				as Iv_User_ActDetailDal;
		}
	}
}