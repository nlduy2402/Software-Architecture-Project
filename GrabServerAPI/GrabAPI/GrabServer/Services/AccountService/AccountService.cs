using GrabServerData;
using Microsoft.EntityFrameworkCore;
using GrabServerCore.Models;
using System.Security.Claims;

namespace GrabServer.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UnitOfWork _unitOfWork;
        public AccountService(IHttpContextAccessor httpContextAccessor, UnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Account> AddAccount(Account acc)
        {
            return await _unitOfWork.AccountRepo.CreateAsync(acc);
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result!;
        }

        public async Task<Account> GetByUsername(string username)
        {
            return await _unitOfWork.AccountRepo.GetByUsername(username);
        }

        public async Task<int> UpdatePositionAccount(string username, double Long, double Lat)
        {
            return await _unitOfWork.AccountRepo.UpdatePositionAccount(username, Long, Lat);
        }

        public async Task<Account> GetById(int id)
        {
            return await _unitOfWork.AccountRepo.GetByIdAsync(id);
        }

        public async Task<Account> UpdateAccount(Account acc)
        {
            return await _unitOfWork.AccountRepo.UpdateAsync(acc);
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

        //public async Task<Account> GetById(int id)
        //{
        //    return await _unitOfWork.AccountRepo.GetByIdAsync(id);
        //}
    }
}
