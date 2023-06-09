﻿using System.Linq.Expressions;
using TakeFramework.Domain.Entities;

namespace TakeFramework.Domain.Repositories
{
    public interface IBaseRepository<T, TPrimaryKey> : IBaseRepository
        where T : BaseEntity<TPrimaryKey>
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
        public Task<(List<T>, int)> PageListAsync(PageRequest pageRequest);

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
        public Task<List<T>> ListAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);


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


        /// <summary>
        /// 获取条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T Single(TPrimaryKey id);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        public (List<T>, int) PageList(PageRequest pageRequest);

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
        public List<T> List();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<T> List(Expression<Func<T, bool>> predicate);


        #endregion

        #endregion


    }

    public interface IBaseRepository { }
}
