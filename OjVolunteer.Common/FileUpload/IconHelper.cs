using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OjVolunteer.Common.FileUpload
{
    public class FileHelper
    {
        public static Boolean ImageUpload(HttpPostedFileBase inFileBase,String dirPath,String filePath, out string fileName)
        {
            try
            {
                dirPath = dirPath + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                filePath = filePath + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string str = Guid.NewGuid().ToString().Substring(1, 10) + ".jpg";
                filePath = filePath + str;
                dirPath = dirPath + str;

                //图片压缩处理
                //Image srcImg = Image.FromStream(inFileBase.InputStream, true, true);
                //Bitmap outFileBase = new Bitmap(srcImg.Width / 3, srcImg.Height / 3);
                //Graphics graphics = Graphics.FromImage(outFileBase);
                //outFileBase.SetResolution(srcImg.HorizontalResolution, srcImg.VerticalResolution); 
                //graphics.DrawImage(srcImg, 0, 0, outFileBase.Width, outFileBase.Height);

                //outFileBase.Save(dirPath);
                //graphics.Dispose();
                //srcImg.Dispose();
                //outFileBase.Dispose();
                inFileBase.SaveAs(dirPath);
                fileName = filePath;
                return true;
            }
            catch {
                fileName = string.Empty;
                return false;
            }
           
            
        }
    }
}
