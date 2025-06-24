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
        Task CreateAsync(SystemAccount account);
        Task UpdateAsync(SystemAccount account);
        Task<bool> DeleteAsync(short id);
        Task<string> GenerateJwtToken(SystemAccount account);

    }
}
