using FUNewsManagementSystem.Data.DTOs;
using FUNewsManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNewsManagementSystem.Business.IServices
{
    public interface IAccountService
    {
        Task<SystemAccount> AuthenticateAsync(string email, string password);
        Task<IEnumerable<SystemAccount>> GetAllAsync();
        Task<SystemAccount> GetByIdAsync(short id);
        Task CreateAsync(SystemAccountRequest account);
        Task UpdateAsync(SystemAccountRequest account);
        Task<bool> DeleteAsync(short id);
        Task<string> GenerateJwtToken(SystemAccount account);

    }
}
