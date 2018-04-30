using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OjVolunteer.Common.FileUpload
{
    public class FileHelper
    {
        public static string IconUpload(HttpPostedFileBase file)
        {
            try
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["DefaultIconSavePath"] + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                string dirPath = Path.GetFullPath(path);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string fileName = path + Guid.NewGuid().ToString().Substring(1, 5) + "-" + file.FileName;
                file.SaveAs(Path.GetFullPath(fileName));
                return fileName;
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}
