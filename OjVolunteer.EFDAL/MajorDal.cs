using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.EFDAL
{
    public class MajorDal
    {
        public OjVolunteerEntities db = new OjVolunteerEntities();

        public IQueryable<major> GetEntities(Expression<Func<major, bool>> whereLambda)
        {
            return db.major.Where(whereLambda).AsQueryable();
        }

        public IQueryable<major> GetPageEntities<S>(int pageSize, int pageIndex, out int total, 
                                                    Expression<Func<major, bool>> whereLambda,
                                                    Expression<Func<major, S>> orderByLambda, 
                                                    bool isAsc)
        {
            total = db.major.Count();
            if (isAsc)
            {
                return db.major.Where(whereLambda).OrderBy<major, S>(orderByLambda).Skip<major>(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                return db.major.Where(whereLambda).OrderByDescending<major, S>(orderByLambda).Skip<major>(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            }
        }

        public bool Update(major entity)
        {
            db.Entry<major>(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool Detele(major entity)
        {
            db.Entry<major>(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return true;
        }

        public major Add(major entity)
        {
            db.major.Add(entity);
            db.SaveChanges();
            return true;
        }
    }
}
