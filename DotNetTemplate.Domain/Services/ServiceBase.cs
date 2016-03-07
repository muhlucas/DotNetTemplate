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

        public virtual void Add(TEntity obj)
        {
            _repository.Add(obj);
        }
        public async void AddAsync(TEntity obj)
        {
            await _repository.AddAsync(obj);
        }

        public virtual void Update(TEntity obj)
        {
            _repository.Update(obj);
        }
        public async virtual void UpdateAsync(TEntity obj)
        {
            await _repository.UpdateAsync(obj);
        }

        public void Remove(TEntity obj)
        {
            _repository.Remove(obj);
        }

        public async void RemoveAsync(TEntity obj)
        {
            await _repository.RemoveAsync(obj);
        }

        public TEntity GetById(long id)
        {
            return _repository.GetById(id);
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public TEntity GetById(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            return _repository.GetById(predicate, includes);
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            return await _repository.GetByIdAsync(predicate, includes);
        }


        public IEnumerable<TEntity> GetAll(params string[] includes)
        {
            return _repository.GetAll(includes);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes)
        {
            return await _repository.GetAllAsync(includes);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> order = null, bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes)
        {
            return _repository.GetAll(predicate, order, reverse, skipRecords, takeRecords, includes);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> order = null, bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes)
        {
            return await _repository.GetAllAsync(predicate, order, reverse, skipRecords, takeRecords, includes);
        }

        public IEnumerable<TEntity> GetAll(ref int totalRecords, Expression<Func<TEntity, bool>> predicate = null,
          Expression<Func<TEntity, object>> order = null, bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes)
        {
            return _repository.GetAll(ref totalRecords, predicate, order, reverse, skipRecords, takeRecords, includes);
        }
    }
}
