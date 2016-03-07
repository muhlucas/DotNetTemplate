using DotNetTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetTemplate.Domain.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity obj);
        Task AddAsync(TEntity obj);

        void Update(TEntity obj);
        Task UpdateAsync(TEntity obj);

        void Remove(TEntity obj);
        Task RemoveAsync(TEntity obj);

        TEntity GetById(long id);
        Task<TEntity> GetByIdAsync(long id);

        TEntity GetById(Expression<Func<TEntity, bool>> predicate, params string[] includes);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);

        IEnumerable<TEntity> GetAll(params string[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> order = null,
            bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> order = null,
                    bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes);
        IEnumerable<TEntity> GetAll(ref int totalRecords, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> order = null,
            bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes);
    }
}
