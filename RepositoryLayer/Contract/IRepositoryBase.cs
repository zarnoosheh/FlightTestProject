using System.Linq.Expressions;

namespace RepositoryLayer.Contract
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindListByCondition(Expression<Func<T, bool>> expression);
        T FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);


        void Update(T entity);
        void Delete(T entity);

        int GetALlFilteredByCount(Expression<Func<T, bool>> expression);
        int GetAllCount();
        void AddRange(List<T> entity);

        T CreateGetEntity(T entity);

        IQueryable<T> FindListByConditionWithIncludes(Expression<Func<T, bool>> expression, Expression<Func<T, object>> include);
    }
}
