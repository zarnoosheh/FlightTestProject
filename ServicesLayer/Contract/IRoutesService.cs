using DataAccessLayer.Entity;
using System.Linq.Expressions;

namespace ServicesLayer.Contract
{
    public interface IRoutesService
    {
        Task<int> GetIdWithCondition(Expression<Func<TblRoutes, bool>> Condition);
        Task<IQueryable<TblRoutes>> FindAll();
        Task<IQueryable<TblRoutes>> FindListByCondition(Expression<Func<TblRoutes, bool>> Condition);
        Task<TblRoutes> FindByCondition(Expression<Func<TblRoutes, bool>> Condition);
        Task<bool> Create(TblRoutes NewTblRoutes);
        Task<bool> Update(TblRoutes UpdateTblRoutes);
        Task<bool> Delete(int Id);
        Task<TblRoutes> CreateGetEntity(TblRoutes NewTblRoutes);
        Task<List<TblRoutes>> FindListByConditionPagenation(Expression<Func<TblRoutes, bool>> Condition, int? page, int? perPage);

        Task<(List<TblFlights>, List<TblRoutes>)> GetFlightWithRoutes(int agencyId, DateTime startDate, DateTime endDate);
    }
}
