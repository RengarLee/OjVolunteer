using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.IDAL
{
    public partial interface IDbSession
    {
        int SaveChanges();
    }
}
