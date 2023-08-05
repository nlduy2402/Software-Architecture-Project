using Microsoft.EntityFrameworkCore;
using GrabServerCore.Common.Helper;
using GrabServerCore.Models;
using GrabServerCore.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GrabServerData.Repositories
{
    public class AccountRepo : RepositoryBase<Account>, IAccountRepo
    {
        private readonly GrabDataContext _dataContext;
        public AccountRepo(GrabDataContext dbContext) : base(dbContext)
        {
            _dataContext = dbContext;
        }
        public async Task<int> UpdatePositionAccount(string username, double Long, double Lat)
        {
            var builder = new StringBuilder(@"EXEC dbo.USP_UpdatePosition ");
            builder.Append($"@Username = \'{username}\', ");
            builder.Append($"@Long = \'{Long}\', ");
            builder.Append($"@Lat = \'{Lat}\' ");
            Console.WriteLine(builder.ToString());
            var result = await _dataContext.Database.ExecuteSqlInterpolatedAsync($"EXECUTE({builder.ToString()})");
            return result;
        }

        public override async Task<Account> CreateAsync(Account acc)
        {
            var builder = new StringBuilder(@"
                DECLARE @result INT
                EXEC @result = dbo.USP_AddAccount ");
            builder.Append($"@Username = \'{acc.Username}\', ");
            builder.Append($"@PasswordHash = \'{acc.PasswordHash}\', ");
            builder.Append($"@PasswordSalt = \'{acc.PasswordSalt}\', ");
            builder.Append($"@UserRole = \'{acc.UserRole}\', ");
            builder.Append($"@RefreshToken = \'{acc.RefreshToken}\', ");
            builder.Append($"@TokenCreated = \'{acc.TokenCreated}\', ");
            builder.Append($"@TokenExpires = \'{acc.TokenExpires}\';\n");

            builder.Append($"EXEC USP_GetAccountById @Id = @result;");
            Console.WriteLine("============================================");
            Console.WriteLine(builder.ToString());
            var result = await _dataContext.Accounts.FromSqlInterpolated($"EXECUTE({builder.ToString()})").ToListAsync();
            return result.FirstOrDefault();
        }

        public async Task<Account> GetByUsername(string username)
        {
            Console.WriteLine("==================================" + username);
            var builder = new StringBuilder($"dbo.USP_GetAccountByUsername @Username = \'{username}\';");

            Console.WriteLine(builder.ToString());
            var result = await _dataContext.Accounts.FromSqlInterpolated($"EXECUTE({builder.ToString()})").ToListAsync();
            return result.FirstOrDefault();
        }

        public override async Task<Account> GetByIdAsync(int id)
        {
            var builder = new StringBuilder($"dbo.USP_GetAccountById @Id = \'{id}\';");

            Console.WriteLine(builder.ToString());
            var result = await _dataContext.Accounts.FromSqlInterpolated($"EXECUTE({builder.ToString()})").ToListAsync();
            return result.FirstOrDefault();
        }
    }
}
