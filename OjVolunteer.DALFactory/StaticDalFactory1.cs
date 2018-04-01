 
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
   
	
		public static IactconditionDal GetactconditionDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".actconditionDal")
				as IactconditionDal;
		}
	
		public static IactenrollDal GetactenrollDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".actenrollDal")
				as IactenrollDal;
		}
	
		public static IactiveindexDal GetactiveindexDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".activeindexDal")
				as IactiveindexDal;
		}
	
		public static IactivitytypeDal GetactivitytypeDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".activitytypeDal")
				as IactivitytypeDal;
		}
	
		public static IactsdetailDal GetactsdetailDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".actsdetailDal")
				as IactsdetailDal;
		}
	
		public static IchangeDal GetchangeDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".changeDal")
				as IchangeDal;
		}
	
		public static IdepartmentDal GetdepartmentDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".departmentDal")
				as IdepartmentDal;
		}
	
		public static IfavorsDal GetfavorsDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".favorsDal")
				as IfavorsDal;
		}
	
		public static ImajorDal GetmajorDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".majorDal")
				as ImajorDal;
		}
	
		public static IorganizeinfosDal GetorganizeinfosDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".organizeinfosDal")
				as IorganizeinfosDal;
		}
	
		public static IpoliticalDal GetpoliticalDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".politicalDal")
				as IpoliticalDal;
		}
	
		public static IstatusDal GetstatusDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".statusDal")
				as IstatusDal;
		}
	
		public static ItalkDal GettalkDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".talkDal")
				as ItalkDal;
		}
	
		public static ItimedetailDal GettimedetailDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".timedetailDal")
				as ItimedetailDal;
		}
	
		public static IuserdurationDal GetuserdurationDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".userdurationDal")
				as IuserdurationDal;
		}
	
		public static IuserinfoDal GetuserinfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".userinfoDal")
				as IuserinfoDal;
		}
	
		public static IuserinteDal GetuserinteDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".userinteDal")
				as IuserinteDal;
		}
	}
}