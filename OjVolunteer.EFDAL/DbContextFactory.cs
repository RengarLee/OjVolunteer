using OjVolunteer.Model;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace OjVolunteer.EFDAL
{
    public static class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {
            DbContext dbContext = CallContext.GetData("DbContext") as DbContext;
            if (dbContext == null)
            {
                dbContext = new OjVolunteerEntities();
                CallContext.SetData("DbContext", dbContext);
            }
            return dbContext;
        }
    }
}