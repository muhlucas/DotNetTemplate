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

        public virtual void Update(TEntity obj)
        {
            obj.UpdatedAt = DateTime.Now;
            Connection.Entry(obj).State = EntityState.Modified;
            Connection.SaveChanges();
        }

        public virtual void Remove(TEntity obj)
        {
            Connection.Entry(obj).State = EntityState.Deleted;
            Connection.Set<TEntity>().Remove(obj);
            Connection.SaveChanges();
        }

        public virtual TEntity GetById(long id, bool lazyLoadEnabled = true)
        {
            Connection.Configuration.LazyLoadingEnabled = lazyLoadEnabled;
            return Connection.Set<TEntity>().Find(id);
        }

        public virtual TEntity GetById(Guid id, bool lazyLoadEnabled = true)
        {
            Connection.Configuration.LazyLoadingEnabled = lazyLoadEnabled;
            return Connection.Set<TEntity>().Find(id);
        }

        public virtual TEntity GetByExpression(Expression<Func<TEntity, bool>> predicate,
            bool lazyLoadEnabled = true, params string[] includes)
        {
            Connection.Configuration.LazyLoadingEnabled = lazyLoadEnabled;
            IQueryable<TEntity> query = Connection.Set<TEntity>();

            if (includes != null && includes.Length > 0 && !String.IsNullOrEmpty(includes.First()))
                Array.ForEach<String>(includes, x => { query = query.Include(x); });

            return query.FirstOrDefault(predicate);

        }

        public virtual IEnumerable<TEntity> GetAll(bool lazyLoadEnabled = true, params string[] includes)
        {
            Connection.Configuration.LazyLoadingEnabled = lazyLoadEnabled;
            IQueryable<TEntity> query = Connection.Set<TEntity>();

            if (includes != null && includes.Length > 0 && !String.IsNullOrEmpty(includes.First()))
                Array.ForEach<String>(includes, x => { query = query.Include(x); });

            return query.ToList();
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> order = null, bool ascending = true, int skipRecords = 0,
            int takeRecords = 0, bool lazyLoadEnabled = true, params string[] includes)
        {
            Connection.Configuration.LazyLoadingEnabled = lazyLoadEnabled;
            IQueryable<TEntity> query = Connection.Set<TEntity>();

            if (includes != null && includes.Length > 0 && !String.IsNullOrEmpty(includes.First()))
                Array.ForEach<String>(includes, x => { query = query.Include(x); });

            query = query.Where(predicate ?? (x => true));
            query = ascending ? order == null ? query.OrderBy(x => x.Id) : query.OrderBy(order) :
                              order == null ? query.OrderByDescending(x => x.Id) : query.OrderByDescending(order);

            return (skipRecords == 0 && takeRecords == 0 ? query :
                query.Skip(skipRecords).Take(takeRecords)).ToList();
        }

        public virtual IEnumerable<TEntity> GetAll(ref int totalRecords, Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> order = null, bool ascending = true, int skipRecords = 0,
            int takeRecords = 0, bool lazyLoadEnabled = true, params string[] includes)
        {
            Connection.Configuration.LazyLoadingEnabled = lazyLoadEnabled;
            IQueryable<TEntity> query = Connection.Set<TEntity>();

            if (includes != null && includes.Length > 0 && !String.IsNullOrEmpty(includes.First()))
                Array.ForEach<String>(includes, x => { query = query.Include(x); });

            query = query.Where(predicate ?? (x => true));
            totalRecords = query.Count();
            query = ascending ? order == null ? query.OrderBy(x => x.Id) : query.OrderBy(order) :
                              order == null ? query.OrderByDescending(x => x.Id) : query.OrderByDescending(order);


            return (skipRecords == 0 && takeRecords == 0 ? query :
                query.Skip(skipRecords).Take(takeRecords)).ToList();
        }

        public void Dispose()
        {
            Connection.Database.Connection.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
