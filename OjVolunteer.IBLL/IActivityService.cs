using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.IBLL
{
    public partial interface IActivityService
    {
        Boolean AddTime(int actId);

        Boolean AddActivity(Activity activity);

        Boolean AddBeforeActivity(Activity activity, List<int> ids);
    }
}
