using OjVolunteer.Model;
using OjVolunteer.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.IBLL
{
    public partial interface ITalksService
    {
        IQueryable<Talks> LoadPageData(TalkQueryParam userQueryParam);
    }
}
