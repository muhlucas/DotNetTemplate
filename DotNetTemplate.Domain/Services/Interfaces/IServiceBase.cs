using DotNetTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetTemplate.Domain.Services.Interfaces
{
    public interface IServiceBase<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity obj);

        void Update(TEntity obj);

        void Remove(TEntity obj);

        TEntity GetById(long id, bool lazyLoadEnabled = true);

        TEntity GetById(Guid id, bool lazyLoadEnabled = true);

        TEntity GetByExpression(Expression<Func<TEntity, bool>> predicate,
            bool lazyLoadEnabled = true, params string[] includes);

        IEnumerable<TEntity> GetAll(bool lazyLoadEnabled = true, params string[] includes);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> order = null, bool ascending = true, int skipRecords = 0,
            int takeRecords = 0, bool lazyLoadEnabled = true, params string[] includes);

        IEnumerable<TEntity> GetAll(ref int totalRecords, Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> order = null, bool ascending = true, int skipRecords = 0,
            int takeRecords = 0, bool lazyLoadEnabled = true, params string[] includes);

    }
}
