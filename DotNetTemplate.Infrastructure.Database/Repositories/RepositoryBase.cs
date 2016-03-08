using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Domain.Repositories.Interfaces;
using DotNetTemplate.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTemplate.Infrastructure.Database.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IDisposable where TEntity : BaseEntity
    {
        protected TemplateContext Connection { get; set; }

        public RepositoryBase(TemplateContext context)
        {
            Connection = context;
        }

        public virtual void Add(TEntity obj)
        {
            Connection.Set<TEntity>().Add(obj);
            Connection.SaveChanges();
        }

        public async virtual Task AddAsync(TEntity obj)
        {
            Connection.Set<TEntity>().Add(obj);
            await Connection.SaveChangesAsync();
        }

        public virtual void Update(TEntity obj)
        {
            obj.UpdatedAt = DateTime.Now;
            Connection.Entry(obj).State = EntityState.Modified;
            Connection.SaveChanges();
        }

        public async virtual Task UpdateAsync(TEntity obj)
        {
            obj.UpdatedAt = DateTime.Now;
            Connection.Entry(obj).State = EntityState.Modified;
            await Connection.SaveChangesAsync();
        }

        public virtual void Remove(TEntity obj)
        {
            Connection.Entry(obj).State = EntityState.Deleted;
            Connection.Set<TEntity>().Remove(obj);
            Connection.SaveChanges();
        }

        public async virtual Task RemoveAsync(TEntity obj)
        {
            Connection.Entry(obj).State = EntityState.Deleted;
            Connection.Set<TEntity>().Remove(obj);
            await Connection.SaveChangesAsync();
        }

        public TEntity GetById(long id)
        {
            return Connection.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await Connection.Set<TEntity>().FindAsync(id);
        }

        public TEntity GetById(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            IQueryable<TEntity> query = Connection.Set<TEntity>();
            if (includes != null && includes.Length > 0)
                Array.ForEach<String>(includes, x => { query = query.Include(x); });
            return query.FirstOrDefault(predicate);
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            IQueryable<TEntity> query = Connection.Set<TEntity>();
            if (includes != null && includes.Length > 0)
                Array.ForEach<String>(includes, x => { query = query.Include(x); });
            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll(params string[] includes)
        {
            IQueryable<TEntity> query = Connection.Set<TEntity>();
            if (includes != null && includes.Length > 0)
                Array.ForEach<String>(includes, x => { query = query.Include(x); });
            return query.ToList();
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes)
        {
            IQueryable<TEntity> query = Connection.Set<TEntity>();
            if (includes != null && includes.Length > 0)
                Array.ForEach<String>(includes, x => { query = query.Include(x); });
            return await query.ToListAsync();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> order = null, bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes)
        {
            IQueryable<TEntity> query = Connection.Set<TEntity>();
            if (includes != null && includes.Length > 0)
                Array.ForEach<String>(includes, x => { query = query.Include(x); });
            query = query.Where(predicate ?? (x => true));
            query = reverse ? order == null ? query.OrderBy(x => x.Id) : query.OrderBy(order) :
                              order == null ? query.OrderByDescending(x => x.Id) : query.OrderByDescending(order);

            return (skipRecords == 0 && takeRecords == 0 ? query :
                query.Skip(skipRecords).Take(takeRecords)).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> order = null, bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes)
        {
            IQueryable<TEntity> query = Connection.Set<TEntity>();
            if (includes != null && includes.Length > 0)
                Array.ForEach<String>(includes, x => { query = query.Include(x); });

            query = query.Where(predicate ?? (x => true));
            query = reverse ? order == null ? query.OrderBy(x => x.Id) : query.OrderBy(order) :
                              order == null ? query.OrderByDescending(x => x.Id) : query.OrderByDescending(order);

            return await (skipRecords == 0 && takeRecords == 0 ? query :
                query.Skip(skipRecords).Take(takeRecords)).ToListAsync();
        }

        public IEnumerable<TEntity> GetAll(ref int totalRecords, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> order = null, bool reverse = false, int skipRecords = 0, int takeRecords = 0, params string[] includes)
        {
            IQueryable<TEntity> query = Connection.Set<TEntity>();
            if (includes != null && includes.Length > 0)
                Array.ForEach<String>(includes, x => { query = query.Include(x); });

            query = query.Where(predicate ?? (x => true));
            totalRecords = query.Count();
            query = reverse ? order == null ? query.OrderBy(x => x.Id) : query.OrderBy(order) :
                              order == null ? query.OrderByDescending(x => x.Id) : query.OrderByDescending(order);


            return (skipRecords == 0 && takeRecords == 0 ? query :
                query.Skip(skipRecords).Take(takeRecords)).ToList();
        }

        public void Dispose()
        {
            Connection.Database.Connection.Close();
            GC.Collect();
        }
    }
}
