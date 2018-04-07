using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.Common.LogWriter
{
    public interface ILogWriter
    {
        void WriteLogInfo(String str);
    }
}
