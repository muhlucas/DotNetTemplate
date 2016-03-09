using DotNetTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetTemplate.Domain.Services.Interfaces
{
    public interface IServiceBase<TEntity> where TEntity : BaseEntity
    {
        void AddAsync(TEntity obj);

        void UpdateAsync(TEntity obj);

        void RemoveAsync(TEntity obj);

        Task<TEntity> GetByIdAsync(long id);

        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);

        Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> order = null,
            bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes);

        IEnumerable<TEntity> GetAll(ref int totalRecords, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> order = null,
           bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes);

    }
}
