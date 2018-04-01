using OjVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OjVolunteer.EFDAL
{
    public class BaseDal<T> where T:class ,new ()
    {
        public DbContext Db { get { return DbContextFactory.GetCurrentDbContext(); } }

        #region 查询
        /// <summary>
        /// 根据Lamdba查找符合的实体集合
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>全部实体的集合</returns>
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().Where(whereLambda).AsQueryable();
        }

        /// <summary>
        /// 根据Lamdba分页查找符合的实体集合
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="total">数据总数</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderByLambda">排序条件</param>
        /// <param name="isAsc">是否为升序</param>
        /// <returns>分页后实体的集合</returns>
        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                    Expression<Func<T, bool>> whereLambda,
                                                    Expression<Func<T, S>> orderByLambda,
                                                    bool isAsc)
        {
            total = Db.Set<T>().Count();
            if (isAsc)
            {
                return Db.Set<T>().Where(whereLambda).OrderBy<T, S>(orderByLambda).Skip<T>(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                return Db.Set<T>().Where(whereLambda).OrderByDescending<T, S>(orderByLambda).Skip<T>(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
            }
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">需要添加的实体</param>
        /// <returns>实体</returns>
        public T Add(T entity)
        {
            Db.Set<T>().Add(entity);
            Db.SaveChanges();
            return entity;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">需要更新的实体</param>
        /// <returns>true</returns>
        public bool Update(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            Db.SaveChanges();
            return true;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">需要删除的实体</param>
        /// <returns>true</returns>
        public bool Detele(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            Db.SaveChanges();
            return true;
        } 
        #endregion


    }
}
