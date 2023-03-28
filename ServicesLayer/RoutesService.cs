using DataAccessLayer.Entity;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Contract;
using ServicesLayer.Contract;
using System.Linq.Expressions;

namespace ServicesLayer.Services
{
    public class RoutesService : IRoutesService
    {

        private readonly IRoutesRepository _TblRoutes;
        private readonly ILogger<RoutesService> _Logger;

        public RoutesService(IRoutesRepository TblRoutes, ILogger<RoutesService> Logger)
        {
            _Logger = Logger;
            _TblRoutes = TblRoutes;
        }


        public async Task<int> GetIdWithCondition(Expression<Func<TblRoutes, bool>> Condition)
        {
            var model = _TblRoutes.FindByCondition(Condition);
            if (model != null)
                return model.Id;

            return 0;
        }

        public async Task<IQueryable<TblRoutes>> FindAll()
        {
            try
            {
                return _TblRoutes.FindAll();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<IQueryable<TblRoutes>> FindListByCondition(Expression<Func<TblRoutes, bool>> Condition)
        {

            try
            {
                return _TblRoutes.FindListByCondition(Condition);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<TblRoutes> FindByCondition(Expression<Func<TblRoutes, bool>> Condition)
        {
            try
            {
                return _TblRoutes.FindByCondition(Condition);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<TblRoutes> GetRoutes(int Id)
        {
            try
            {
                return _TblRoutes.FindByCondition(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }
        public async Task<bool> Create(TblRoutes NewTblRoutes)
        {
            try
            {
                _TblRoutes.Create(NewTblRoutes);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return false;
            }
        }
        public async Task<TblRoutes> CreateGetEntity(TblRoutes NewTblRoutes)
        {
            try
            {
                return _TblRoutes.CreateGetEntity(NewTblRoutes);

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<bool> Update(TblRoutes UpdateTblRoutes)
        {
            try
            {
                TblRoutes FindTblRoutes = await GetRoutes(UpdateTblRoutes.Id);
                if (FindTblRoutes != null)
                {
                    _TblRoutes.Update(UpdateTblRoutes);
                }
                else
                {
                    Console.Write($"This TblRoutes Is not Exist");
                    _Logger.LogInformation($"This TblRoutes Is not Exist");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return false;
            }
        }
        public async Task<bool> Delete(int Id)
        {
            try
            {
                TblRoutes FindTblRoutes = await GetRoutes(Id);
                if (FindTblRoutes != null)
                {
                    _TblRoutes.Delete(FindTblRoutes);
                }
                else
                {
                    Console.Write($"This TblRoutes Is not Exist");
                    _Logger.LogInformation($"This TblRoutes Is not Exist");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return false;
            }
        }


        public async Task<List<TblRoutes>> FindListByConditionPagenation(Expression<Func<TblRoutes, bool>> Condition, int? page, int? perPage)
        {
            try
            {
                return _TblRoutes.FindListByCondition(Condition)
                    .Skip(page.HasValue ? page.Value : 0)
                    .Take(perPage.HasValue ? perPage.Value : 10)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }

        public async Task<(List<TblFlights>, List<TblRoutes>)> GetFlightWithRoutes(int agencyId, DateTime startDate, DateTime endDate)
        {
            var (flights, Routes) = await _TblRoutes.GetFlightWithRoutes(agencyId, startDate, endDate);
            return (flights, Routes);
        }

    }
}
