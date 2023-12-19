using System.Linq.Expressions;
using TakeFramework.Domain.Entities;

namespace TakeFramework.Domain.Repositories
{
    public interface IBaseRepository<T, TPrimaryKey> : IBaseRepository
        where T : class, IEntity<TPrimaryKey>
    {

        #region Task/async

        #region Create


        /// <summary>
        /// 新建数据
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public Task<T> CreateAsync(T intput);
        /// <summary>
        /// 批量新建数据
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public Task CreateAsync(List<T> intput);

        #endregion


        #region Update


        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public Task<T> UpdateAsync(T intput);
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public Task UpdateAsync(List<T> intput);
        /// <summary>
        /// 根据条件修改数据
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public Task UpdateAsync(T intput, Expression<Func<T, bool>> predicate);

        #endregion

        #region Delete

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task DeleteAsync(T intput);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task DeleteAsync(List<T> intput);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task DeleteAsync(TPrimaryKey intput);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task DeleteAsync(List<TPrimaryKey> intput);
        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task DeleteAsync(Expression<Func<T, bool>> predicate);


        #endregion

        #region Select

        /// <summary>
        /// 根据表达式获取第一条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 获取条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task<T> SingleAsync(TPrimaryKey id);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        public Task<(List<T> data, int totalCount)> PageListAsync(PageRequest pageRequest, bool isTracking = false);

        public Task<(List<T> data, int totalCount)> PageListAsync(int sikpCount, int pageSize, string orderField, Expression<Func<T, bool>>? predicate = null);

        /// <summary>
        /// 根据表达式获取第一条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<T?> FistOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<T>> ListAsync(Expression<Func<T, bool>>? predicate = null, bool isTracking = false);

        public Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
        public Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null);
        #endregion

        #endregion

        #region Synchronous

        #region Create


        /// <summary>
        /// 新建数据
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public T Create(T intput);
        /// <summary>
        /// 批量新建数据
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public void Create(List<T> intput);

        #endregion


        #region Update


        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public abstract T Update(T intput);
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public void Update(List<T> intput);
        /// <summary>
        /// 根据条件修改数据
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public void Update(T intput, Expression<Func<T, bool>> predicate);

        #endregion

        #region Delete

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public abstract void Delete(T intput);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void Delete(List<T> intput);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void Delete(TPrimaryKey intput);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void Delete(List<TPrimaryKey> intput);
        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public void Delete(Expression<Func<T, bool>> predicate);
        #endregion

        #region Select
        public T Single(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 获取条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T Single(TPrimaryKey id);
        /// <summary>
        /// 根据表达式获取第一条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T? SingleOrDefault(Expression<Func<T, bool>>? predicate);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        public (List<T> data, int totalCount) PageList(PageRequest pageRequest, bool isTracking = false);
        public (List<T> data, int totalCount) PageList(int sikpCount, int pageSize, string orderField, Expression<Func<T, bool>>? predicate = null);

        /// <summary>
        /// 根据表达式获取第一条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T? FistOrDefault(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<T> List(Expression<Func<T, bool>>? predicate = null, bool isTracking = false);

        public int Count(Expression<Func<T, bool>>? predicate = null);
        public bool Any(Expression<Func<T, bool>>? predicate = null);

        #endregion

        #endregion

    }
    public interface IBaseRepository { }
}
