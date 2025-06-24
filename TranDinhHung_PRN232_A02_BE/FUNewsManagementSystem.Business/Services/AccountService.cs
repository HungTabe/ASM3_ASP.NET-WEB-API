using FUNewsManagementSystem.Business.IServices;
using FUNewsManagementSystem.Data.Entities;
using FUNewsManagementSystem.Data.IRepositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNewsManagementSystem.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly ISystemAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        public AccountService(ISystemAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
        }

        public async Task<SystemAccount> AuthenticateAsync(string email, string password)
        {
            var accounts = await _accountRepository.GetAllAsync();
            var account = accounts.FirstOrDefault(a => a.AccountEmail == email && a.AccountPassword == password);

            if (account == null)
            {
                var adminEmail = _configuration["AdminAccount:Email"];
                var adminPassword = _configuration["AdminAccount:Password"];
                if (email == adminEmail && password == adminPassword)
                {
                    return new SystemAccount
                    {
                        AccountEmail = adminEmail,
                        AccountRole = 0 // Admin role
                    };
                }
            }

            return account;
        }

        public async Task<IEnumerable<SystemAccount>> GetAllAsync() => await _accountRepository.GetAllAsync();
        public async Task<SystemAccount> GetByIdAsync(short id) => await _accountRepository.GetByIdAsync(id);
        public async Task CreateAsync(SystemAccount account) => await _accountRepository.CreateAsync(account);
        public async Task UpdateAsync(SystemAccount account) => await _accountRepository.UpdateAsync(account);
        public async Task<bool> DeleteAsync(short id) => await _accountRepository.DeleteAsync(id);
    }
}
