using DataAccessLayer.Entity;
using System.Linq.Expressions;

namespace ServicesLayer.Contract
{
    public interface IFlightService
    {
        Task<int> GetIdWithCondition(Expression<Func<TblFlights, bool>> Condition);
        Task<IQueryable<TblFlights>> FindAll();
        Task<IQueryable<TblFlights>> FindListByCondition(Expression<Func<TblFlights, bool>> Condition);
        Task<TblFlights> FindByCondition(Expression<Func<TblFlights, bool>> Condition);
        Task<bool> Create(TblFlights NewTblFlights);
        Task<bool> Update(TblFlights UpdateTblFlights);
        Task<bool> Delete(int Id);
        Task<TblFlights> CreateGetEntity(TblFlights NewTblFlights);
        Task<List<TblFlights>> FindListByConditionPagenation(Expression<Func<TblFlights, bool>> Condition, int? page, int? perPage);
    }
}
