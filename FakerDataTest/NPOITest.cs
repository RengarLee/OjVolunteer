using OjVolunteer.DALFactory;
using OjVolunteer.EFDAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerDataTest
{
    public class NPOITest
    {
        public void DataExportToExecl()

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
    }
}
