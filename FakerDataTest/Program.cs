using OjVolunteer.Common.Encryption;
using OjVolunteer.DALFactory;
using OjVolunteer.EFDAL;
using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakerDataTest
{
    class Program
    {

        static void Main(string[] args)
        {

            //专业
            //GenerateMajor(20);
            //学院
            //GenerateDepartment(20);
            //组织
            //GenerateOrganizeInfo(10);

            //用户
            DataExportToExecl();
            // GenerateUserInfo(100);

            Thread.Sleep(60000);
            DeleteExecl();

            Console.Write("ok");
            Console.Read();
        }

        static void DeleteExecl()
        {
            File.Delete("test.xls");
        }

        static void DataExportToExecl()
        {
            OrganizeInfoDal dal = new OrganizeInfoDal();
            DbSession dbSession = new DbSession();
            //创建Excel文件的对象

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1"); //添加一个sheet
            var _data = dal.GetEntities(u => true).ToList();

            //给sheet1添加第一行的头部标题

            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);

            row1.CreateCell(0).SetCellValue("排名");

            row1.CreateCell(1).SetCellValue("CardID");

            row1.CreateCell(2).SetCellValue("姓名");

            row1.CreateCell(3).SetCellValue("手机");

            row1.CreateCell(4).SetCellValue("职位");

            row1.CreateCell(5).SetCellValue("所在公司");

            row1.CreateCell(6).SetCellValue("创建时间");
            //将数据逐步写入sheet1各个行

            for (int i = 0; i < _data.Count; i++)

            {

                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);

                rowtemp.CreateCell(0).SetCellValue(i + 1);

                rowtemp.CreateCell(1).SetCellValue(_data[i].OrganizeInfoShowName);

            }

            // 写入到客户端 

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            book.Write(ms);

            ms.Seek(0, System.IO.SeekOrigin.Begin);
            var buf = ms.ToArray();

            using (FileStream fs = new FileStream("test.xls", FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }

        }

        //专业
        static void GenerateMajor(int num)
        {
            MajorDal dal = new MajorDal();
            DbSession dbSession = new DbSession();
            for (int i = 0; i < num; i++)
            {
                Major major = new Major();

                major.MajorName = "Major_____" + i.ToString();
                major.ModfiedOn = Convert.ToDateTime(Faker.Business.CreditCardExpiryDate());
                major.CreateTime = major.ModfiedOn.Value.AddMonths(-3);
                major.Status = Faker.Boolean.Next() ? (short)1 : (short)0;
                dal.Add(major);
            }
            dbSession.SaveChanges();
        }

        //学院
        static void GenerateDepartment(int num)
        {
            DepartmentDal dal = new DepartmentDal();
            DbSession dbSession = new DbSession();
            for (int i = 0; i < num; i++)
            {
                Department major = new Department();

                major.DepartmentName = "Department_____" + i.ToString();
                major.ModfiedOn = Convert.ToDateTime(Faker.Business.CreditCardExpiryDate());
                major.CreateTime = major.ModfiedOn.Value.AddMonths(-3);
                major.Status = Faker.Boolean.Next() ? (short)1 : (short)0;
                dal.Add(major);
            }
            dbSession.SaveChanges();
        }
        
        //组织
        static void GenerateOrganizeInfo(int num)
        {

            OrganizeInfoDal dal = new OrganizeInfoDal();
            DbSession dbSession = new DbSession();
            for (int i = 0; i < num; i++)
            {
                OrganizeInfo organize = new OrganizeInfo();
                organize.OrganizeInfoLoginId = Guid.NewGuid().ToString().Substring(0, 10);
                organize.OrganizeInfoShowName = (Faker.Name.First() + Faker.Name.First()).Substring(3);
                organize.OrganizeInfoPwd = MD5Helper.Get_MD5(Faker.Name.First()).Substring(3);
                organize.OrganizeInfoPeople = (Faker.Name.Last() + Faker.Name.First()).Substring(3);
                organize.OrganizeInfoEmail = Faker.Internet.FreeEmail().Substring(0,10);
                organize.OrganizeInfoManageId = 2;
                
                organize.OrganizeInfoLastTime = Convert.ToDateTime(Faker.Business.CreditCardExpiryDate());
                organize.CreateTime = organize.OrganizeInfoLastTime.AddMonths(-10);
                organize.ModfiedOn = organize.OrganizeInfoLastTime.AddMonths(-4);
                organize.OrganizeInfoIcon = "/Content/Upload/images/1.jpg";
                organize.ActivityCount = 0;
                organize.Status = (short)((new Random().Next(9)) % 3);
                dal.Add(organize);
                
            }
            dbSession.SaveChanges();
        }

        static void GenerateUserInfo(int num)
        {
            UserInfoDal dal = new UserInfoDal();
            DbSession dbSession = new DbSession();
            for (int i = 0; i < num; i++)
            {
                UserInfo user = new UserInfo();
                user.UserInfoLoginId = Guid.NewGuid().ToString().Substring(0, 10);
                user.UserInfoShowName = (Faker.Name.First() + Faker.Name.First()).Substring(3);
                user.UserInfoPwd = MD5Helper.Get_MD5(Faker.Name.First()).Substring(3);
                user.UserInfoStuId = Faker.Phone.Extension()+ Faker.Phone.Extension();
                user.MajorID = new Random().Next(1, 5);
                user.UserInfoEmail = Faker.Internet.FreeEmail().Substring(0, 10);
                user.OrganizeInfoID = new Random().Next(3,10);
                user.PoliticalID = new Random().Next(1,12);
                user.DepartmentID = new Random().Next(2,7);
                user.UserInfoTalkCount = new Random().Next(100);
                user.UserInfoIcon = "/Content/Upload/images/1.jpg";
                user.UserInfoLastTime = Convert.ToDateTime(Faker.Business.CreditCardExpiryDate());
                user.CreateTime = user.UserInfoLastTime.Value.AddMonths(-10);
                user.ModfiedOn = user.UserInfoLastTime.Value.AddMonths(-4);
                user.Status = (short)((new Random().Next(9)) % 3);
                dal.Add(user);

            }
            dbSession.SaveChanges();
        }

        static void ShowFakeUserInfo()
        {
            Console.WriteLine(String.Format("User: {0}", Faker.Name.FullName()));
            Console.WriteLine(String.Format("Phone: {0}", Faker.Phone.Number()));
            Console.WriteLine(String.Format("Email1: {0}", Faker.Internet.Email()));
            Console.WriteLine(String.Format("Email2: {0}", Faker.Internet.FreeEmail()));
            Console.WriteLine(String.Format("Company: {0}", Faker.Company.Name()));
            Console.WriteLine(String.Format("Country: {0}", Faker.Address.Country()));
        }
    }
}
