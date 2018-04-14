using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.IDAL
{
    public interface IBaseDal<T> where T:class, new()
    {
        #region 查询
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda);

        IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                    Expression<Func<T, bool>> whereLambda,
                                                    Expression<Func<T, S>> orderByLambda,
                                                    bool isAsc);

        #endregion

        #region 添加
        T Add(T entity);
        #endregion

        #region 更新
        bool Update(T entity);
        #endregion

        #region 删除
        bool Delete(T entity);
        bool Delete(int id);
        //批量删除
        int DeleteListByLogical(List<int> ids);
        #endregion

    }
}
