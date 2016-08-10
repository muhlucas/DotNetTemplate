using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Domain.Repositories.Interfaces;
using DotNetTemplate.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetTemplate.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : BaseEntity
    {
        internal IRepositoryBase<TEntity> _repository { get; set; }

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public void Add(TEntity obj)
        {
            obj.CreatedAt = DateTime.Now;
            _repository.Add(obj);
        }

        public void Update(TEntity obj)
        {
            obj.UpdatedAt = DateTime.Now;
            _repository.Update(obj);
        }

        public void Remove(TEntity obj)
        {
            _repository.Remove(obj);
        }

        public TEntity GetById(long id, bool lazyLoadEnabled = true)
        {
            return _repository.GetById(id, lazyLoadEnabled);
        }

        public TEntity GetById(Guid id, bool lazyLoadEnabled = true)
        {
            return _repository.GetById(id, lazyLoadEnabled);
        }

        public TEntity GetByExpression(Expression<Func<TEntity, bool>> predicate, bool lazyLoadEnabled = true, params string[] includes)
        {
            return _repository.GetByExpression(predicate, lazyLoadEnabled, includes);
        }

        public IEnumerable<TEntity> GetAll(bool lazyLoadEnabled = true, params string[] includes)
        {
            return _repository.GetAll(lazyLoadEnabled, includes);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> order = null, bool ascending = true, int skipRecords = 0,
            int takeRecords = 0, bool lazyLoadEnabled = true, params string[] includes)
        {
            return _repository.GetAll(predicate, order, ascending, skipRecords, takeRecords, lazyLoadEnabled, includes);
        }

        public IEnumerable<TEntity> GetAll(ref int totalRecords, Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> order = null, bool ascending = true, int skipRecords = 0,
            int takeRecords = 0, bool lazyLoadEnabled = true, params string[] includes)
        {
            return _repository.GetAll(ref totalRecords, predicate, order, ascending, skipRecords, takeRecords, lazyLoadEnabled, includes);
        }
    }
}
