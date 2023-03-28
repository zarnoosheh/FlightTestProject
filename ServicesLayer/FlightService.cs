using DataAccessLayer.Entity;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Contract;
using ServicesLayer.Contract;
using System.Linq.Expressions;

namespace ServicesLayer.Services
{
    public class FlightService : IFlightService
    {

        private readonly IFlightRepository _TblFlights;
        private readonly ILogger<FlightService> _Logger;

        public FlightService(IFlightRepository TblFlights, ILogger<FlightService> Logger)
        {
            _Logger = Logger;
            _TblFlights = TblFlights;
        }


        public async Task<int> GetIdWithCondition(Expression<Func<TblFlights, bool>> Condition)
        {
            var model = _TblFlights.FindByCondition(Condition);
            if (model != null)
                return model.Id;

            return 0;
        }

        public async Task<IQueryable<TblFlights>> FindAll()
        {
            try
            {
                return _TblFlights.FindAll();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<IQueryable<TblFlights>> FindListByCondition(Expression<Func<TblFlights, bool>> Condition)
        {

            try
            {
                return _TblFlights.FindListByCondition(Condition);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }

        public async Task<TblFlights> FindByCondition(Expression<Func<TblFlights, bool>> Condition)
        {
            try
            {
                return _TblFlights.FindByCondition(Condition);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<TblFlights> GetFlight(int Id)
        {
            try
            {
                return _TblFlights.FindByCondition(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }
        public async Task<bool> Create(TblFlights NewTblFlights)
        {
            try
            {
                _TblFlights.Create(NewTblFlights);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return false;
            }
        }
        public async Task<TblFlights> CreateGetEntity(TblFlights NewTblFlights)
        {
            try
            {
                return _TblFlights.CreateGetEntity(NewTblFlights);

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<bool> Update(TblFlights UpdateTblFlights)
        {
            try
            {
                TblFlights FindTblFlights = await GetFlight(UpdateTblFlights.Id);
                if (FindTblFlights != null)
                {
                    _TblFlights.Update(UpdateTblFlights);
                }
                else
                {
                    Console.Write($"This TblFlights Is not Exist");
                    _Logger.LogInformation($"This TblFlights Is not Exist");
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
                TblFlights FindTblFlights = await GetFlight(Id);
                if (FindTblFlights != null)
                {
                    _TblFlights.Delete(FindTblFlights);
                }
                else
                {
                    Console.Write($"This TblFlights Is not Exist");
                    _Logger.LogInformation($"This TblFlights Is not Exist");
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


        public async Task<List<TblFlights>> FindListByConditionPagenation(Expression<Func<TblFlights, bool>> Condition, int? page, int? perPage)
        {
            try
            {
                return _TblFlights.FindListByCondition(Condition)
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




    }
}
