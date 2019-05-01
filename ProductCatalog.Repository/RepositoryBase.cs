using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.BusinessEntities;
using ProductCatalog.Contracts;

namespace ProductCatalog.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ProductRepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(ProductRepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        
        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await this.RepositoryContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return this.RepositoryContext.Set<T>();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>()
              .Where(expression);
        }

    }
}
