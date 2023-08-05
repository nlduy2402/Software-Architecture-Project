using GrabServerCore.Models;

namespace GrabServer.Services.AccountService
{
    public interface IAccountService
    {
        string GetMyName();
        Task<Account> AddAccount(Account acc);
        Task<Account> GetByUsername(string username);
        Task<Account> GetById(int id);
        Task<Account> UpdateAccount(Account acc);
        Task<int> UpdatePositionAccount(string username, double Long, double Lat);
        void SaveChanges();
    }
}
