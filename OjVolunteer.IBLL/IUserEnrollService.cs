using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.IBLL
{
    public partial interface  IUserEnrollService
    {
        bool SignIn(int aId, List<int> uIdList);

        bool SignOut(int aId, List<int> uIdList);
    }
}
