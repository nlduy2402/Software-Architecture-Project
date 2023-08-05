using GrabServerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabServerCore.RepoInterfaces
{
    public interface IAccountRepo : IRepository<Account>
    {
        Task<Account> GetByUsername(string username);
        Task<int> UpdatePositionAccount(string username, double Long, double Lat);
    }
}
