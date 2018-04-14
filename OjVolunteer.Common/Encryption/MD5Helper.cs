using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Common.Encryption
{
    public class MD5Helper
    {
        public static string Get_MD5(string strSource)
        {
            byte[] buffer = System.Text.Encoding.Default.GetBytes(strSource);

            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytResult = md5.ComputeHash(buffer);
            string strResult = BitConverter.ToString(bytResult);
            strResult = strResult.Replace("-", "");

            return strResult.ToLower();  
        }
    }
}
