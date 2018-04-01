using OjVolunteer.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.BLL
{
    public class BaseService<T> where T: class, new ()
    {
        public IBaseDal<T> CurrentDal { get; set; }


        #region 查询
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
         
        }
        IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                            Expression<Func<T, bool>> whereLambda,
                                            Expression<Func<T, S>> orderByLambda,
                                            bool isAsc)
        { }
        #endregion

        #region 添加
        public T Add(T entity)

        {
            return entity;
        }
        #endregion

        #region 更新
        public bool Update(T entity) {
            return true;
                }
        #endregion

        #region 删除
        public bool Delete(T entity) {
            return true;
        }
        #endregion

    }
}
