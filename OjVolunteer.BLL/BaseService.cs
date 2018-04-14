using OjVolunteer.DALFactory;
using OjVolunteer.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.BLL
{
    public abstract class BaseService<T> where T: class, new ()
    {
        public IBaseDal<T> CurrentDal { get; set; }

        public IDbSession DbSession
        {
            get { return DbSessionFactory.GetCurrentDbSession(); }
        }

        public abstract void SetCurrentDal();

        public BaseService()
        {
            SetCurrentDal();
        }

        #region 查询
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.GetEntities(whereLambda);
        }
        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                            Expression<Func<T, bool>> whereLambda,
                                            Expression<Func<T, S>> orderByLambda,
                                            bool isAsc)
        {
            return CurrentDal.GetPageEntities(pageSize, pageIndex, out total, whereLambda, orderByLambda, isAsc);      
        }
        #endregion

        #region 添加
        public T Add(T entity)
        {
            CurrentDal.Add(entity);
            DbSession.SaveChanges();
            return entity;
        }
        #endregion

        #region 更新
        public bool Update(T entity)
        {
            CurrentDal.Update(entity);
            return DbSession.SaveChanges()>0;
        }
        #endregion

        #region 删除
        public bool Delete(T entity)
        {
            CurrentDal.Detele(entity);
            return DbSession.SaveChanges()>0;
        }

        public int DeleteListByLogical(List<int> ids)
        {
            CurrentDal.DeleteListByLogical(ids);
            return DbSession.SaveChanges();
        }
        #endregion

    }
}
