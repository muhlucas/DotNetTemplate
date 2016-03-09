using DotNetTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetTemplate.Domain.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity obj);

        Task UpdateAsync(TEntity obj);
        
        Task RemoveAsync(TEntity obj);

        Task<TEntity> GetByIdAsync(long id);

        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);
        
        Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> order = null,
                    bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes);

        IEnumerable<TEntity> GetAll(ref int totalRecords, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> order = null,
            bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes);
    }
}
