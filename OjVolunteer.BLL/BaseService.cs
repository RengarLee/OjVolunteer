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
            try
            {
                return CurrentDal.GetEntities(whereLambda);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                            Expression<Func<T, bool>> whereLambda,
                                            Expression<Func<T, S>> orderByLambda,
                                            bool isAsc)
        {
            try
            {
                return CurrentDal.GetPageEntities(pageSize, pageIndex, out total, whereLambda, orderByLambda, isAsc);
            }
            catch (Exception e)
            {
                total = 0;
                return null;
            }
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
            return DbSession.SaveChanges() > 0;
        }

        public int UpdateListStatus(List<int> ids, short delFlag)
        {
            CurrentDal.UpdateListStatus(ids, delFlag);
            return DbSession.SaveChanges();
        }

        /// <summary>
        /// 更新列表数据的Status,使其为正常
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int NormalListByULS(List<int> ids)
        {
            CurrentDal.UpdateListStatus(ids, (short)Model.Enum.DelFlagEnum.Normal);
            return DbSession.SaveChanges();
        }

        #endregion

        #region 删除
        public bool Delete(T entity)
        {
            CurrentDal.Delete(entity);
            return DbSession.SaveChanges()>0;
        }

        public bool Delete(int id)
        {
            CurrentDal.Delete(id);
            return DbSession.SaveChanges()>0;
        }

        public int DeleteListByLogical(List<int> ids)
        {
            CurrentDal.DeleteListByLogical(ids);
            return DbSession.SaveChanges();
        }

        public int DeleteListByULS(List<int> ids)
        {
            CurrentDal.UpdateListStatus(ids, (short)Model.Enum.DelFlagEnum.Deleted);
            return DbSession.SaveChanges();
        }
        #endregion

    }
}
