using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.DALFactory
{
    public partial class StaticDalFactory
    {
        private static String assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
    }
}
