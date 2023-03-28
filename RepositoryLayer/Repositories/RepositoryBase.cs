using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contract;
using System.Linq.Expressions;

namespace RepositoryLayer.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationContext RepositoryContext { get; set; }
        public RepositoryBase(ApplicationContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }
        public IQueryable<T> FindAll()
        {

            return this.RepositoryContext.Set<T>().AsQueryable();

        }
        public IQueryable<T> FindListByCondition(Expression<Func<T, bool>> expression)
        {

            return this.RepositoryContext.Set<T>().Where(expression).AsQueryable();

        }

        public IQueryable<T> FindListByConditionWithIncludes(Expression<Func<T, bool>> expression, Expression<Func<T, object>> include)
        {
            return this.RepositoryContext.Set<T>().Where(expression).Include(include).AsQueryable();
        }


        public T FindByCondition(Expression<Func<T, bool>> expression)
        {

#pragma warning disable CS8603 // Possible null reference return.
            return this.RepositoryContext.Set<T>().FirstOrDefault(expression);
#pragma warning restore CS8603 // Possible null reference return.

        }

        public void Create(T entity)
        {


            this.RepositoryContext.Set<T>().Add(entity);
            this.RepositoryContext.SaveChanges();


        }
        public void AddRange(List<T> entity)
        {

            this.RepositoryContext.Set<T>().AddRange(entity);
            this.RepositoryContext.SaveChanges();


        }
        public void Update(T entity)
        {

            this.RepositoryContext.Entry<T>(entity).State = EntityState.Detached;
            this.RepositoryContext.Entry<T>(entity).State = EntityState.Modified;
            this.RepositoryContext.Set<T>().Update(entity)/*.Property("IntId").IsModified = false*/;
            this.RepositoryContext.SaveChanges();

        }
        public void Delete(T entity)
        {

            this.RepositoryContext.Set<T>().Remove(entity);
            this.RepositoryContext.SaveChanges();

        }

        public int GetALlFilteredByCount(Expression<Func<T, bool>> expression)
        {

            var pi = typeof(T).GetProperty("Id", System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return this.RepositoryContext.Set<T>().Where(expression).Select(a => pi.GetValue(a, null)).Count();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        }


        public int GetAllCount()
        {

            var pi = typeof(T).GetProperty("Id", System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            return this.RepositoryContext.Set<T>().Count();

        }

        public T CreateGetEntity(T entity)
        {
            var Entity = this.RepositoryContext.Set<T>().Add(entity).Entity;
            this.RepositoryContext.SaveChanges();
            return Entity;
        }


    }
}
