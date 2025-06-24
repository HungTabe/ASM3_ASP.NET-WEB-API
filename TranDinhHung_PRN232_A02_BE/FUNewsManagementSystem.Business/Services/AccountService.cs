using FUNewsManagementSystem.Business.IServices;
using FUNewsManagementSystem.Data.DTOs;
using FUNewsManagementSystem.Data.Entities;
using FUNewsManagementSystem.Data.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        public async Task CreateAsync(SystemAccountRequest accountDto)
        {
            // Check SystemAccount Exist ?
            var user = await _accountRepository.GetByNameOrEmailAsync(accountDto.AccountName, accountDto.AccountEmail);
            if (user != null)
            {
                throw new Exception("UserName or UseEmail has existed in database");
            }

            var account = new SystemAccount
            {
                AccountName = accountDto.AccountName,
                AccountEmail = accountDto.AccountEmail,
                AccountRole = accountDto.AccountRole,
                AccountPassword = accountDto.AccountPassword
            };
            await _accountRepository.ICreateAsync(account);
        }

        public async Task UpdateAsync(SystemAccountRequest accountDto)
        {
            var account = new SystemAccount
            {
                AccountName = accountDto.AccountName,
                AccountEmail = accountDto.AccountEmail,
                AccountRole = accountDto.AccountRole,
                AccountPassword = accountDto.AccountPassword
            };
            await _accountRepository.IUpdateAsync(account);
        }
        public async Task<bool> DeleteAsync(short id) => await _accountRepository.DeleteAsync(id);

        public async Task<string> GenerateJwtToken(SystemAccount account)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, account.AccountEmail),
                new Claim(ClaimTypes.Role, account.AccountRole.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
