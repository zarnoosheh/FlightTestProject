using DataAccessLayer.Entity;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Contract;
using ServicesLayer.Contract;
using System.Linq.Expressions;

namespace ServicesLayer.Services
{
    public class SubscriptionsService : ISubscriptionsService
    {

        private readonly ISubscriptionsRepository _TblSubscriptions;
        private readonly ILogger<SubscriptionsService> _Logger;

        public SubscriptionsService(ISubscriptionsRepository TblSubscriptions, ILogger<SubscriptionsService> Logger)
        {
            _Logger = Logger;
            _TblSubscriptions = TblSubscriptions;
        }


        public async Task<int> GetIdWithCondition(Expression<Func<TblSubscriptions, bool>> Condition)
        {
            var model = _TblSubscriptions.FindByCondition(Condition);
            if (model != null)
                return model.Id;

            return 0;
        }

        public async Task<IQueryable<TblSubscriptions>> FindAll()
        {
            try
            {
                return _TblSubscriptions.FindAll();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<IQueryable<TblSubscriptions>> FindListByCondition(Expression<Func<TblSubscriptions, bool>> Condition)
        {

            try
            {
                return _TblSubscriptions.FindListByCondition(Condition);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<TblSubscriptions> FindByCondition(Expression<Func<TblSubscriptions, bool>> Condition)
        {
            try
            {
                return _TblSubscriptions.FindByCondition(Condition);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<TblSubscriptions> GetSubscriptions(int Id)
        {
            try
            {
                return _TblSubscriptions.FindByCondition(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }
        public async Task<bool> Create(TblSubscriptions NewTblSubscriptions)
        {
            try
            {
                _TblSubscriptions.Create(NewTblSubscriptions);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return false;
            }
        }
        public async Task<TblSubscriptions> CreateGetEntity(TblSubscriptions NewTblSubscriptions)
        {
            try
            {
                return _TblSubscriptions.CreateGetEntity(NewTblSubscriptions);

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _Logger.LogInformation(ex.Message);
                return null;
            }
        }
        public async Task<bool> Update(TblSubscriptions UpdateTblSubscriptions)
        {
            try
            {
                TblSubscriptions FindTblSubscriptions = await GetSubscriptions(UpdateTblSubscriptions.Id);
                if (FindTblSubscriptions != null)
                {
                    _TblSubscriptions.Update(UpdateTblSubscriptions);
                }
                else
                {
                    Console.Write($"This TblSubscriptions Is not Exist");
                    _Logger.LogInformation($"This TblSubscriptions Is not Exist");
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
                TblSubscriptions FindTblSubscriptions = await GetSubscriptions(Id);
                if (FindTblSubscriptions != null)
                {
                    _TblSubscriptions.Delete(FindTblSubscriptions);
                }
                else
                {
                    Console.Write($"This TblSubscriptions Is not Exist");
                    _Logger.LogInformation($"This TblSubscriptions Is not Exist");
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


        public async Task<List<TblSubscriptions>> FindListByConditionPagenation(Expression<Func<TblSubscriptions, bool>> Condition, int? page, int? perPage)
        {
            try
            {
                return _TblSubscriptions.FindListByCondition(Condition)
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
