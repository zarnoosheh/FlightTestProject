using DataAccessLayer.Entity;
using System.Linq.Expressions;

namespace ServicesLayer.Contract
{
    public interface ISubscriptionsService
    {
        Task<int> GetIdWithCondition(Expression<Func<TblSubscriptions, bool>> Condition);
        Task<IQueryable<TblSubscriptions>> FindAll();
        Task<IQueryable<TblSubscriptions>> FindListByCondition(Expression<Func<TblSubscriptions, bool>> Condition);
        Task<TblSubscriptions> FindByCondition(Expression<Func<TblSubscriptions, bool>> Condition);
        Task<bool> Create(TblSubscriptions NewTblSubscriptions);
        Task<bool> Update(TblSubscriptions UpdateTblSubscriptions);
        Task<bool> Delete(int Id);
        Task<TblSubscriptions> CreateGetEntity(TblSubscriptions NewTblSubscriptions);
        Task<List<TblSubscriptions>> FindListByConditionPagenation(Expression<Func<TblSubscriptions, bool>> Condition, int? page, int? perPage);
    }
}
