using OjVolunteer.EFDAL;
using OjVolunteer.IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.DALFactory
{
    public partial class DbSession : IDbSession
    {
        public int SaveChanges()
        {
            try
            {
                return DbContextFactory.GetCurrentDbContext().SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                return 0;
                throw;
            }
        }
    }
}
