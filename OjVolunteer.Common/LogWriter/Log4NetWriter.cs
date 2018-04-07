using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Common.LogWriter
{
    /// <summary>
    /// 将错误日志通过Log4Net记录
    /// </summary>
    public class Log4NetWriter:ILogWriter
    {
        public void WriteLogInfo(String str)
        {
            ILog logWriter = log4net.LogManager.GetLogger("OjVolunteer");
            logWriter.Error(str);
        }
    }
}
