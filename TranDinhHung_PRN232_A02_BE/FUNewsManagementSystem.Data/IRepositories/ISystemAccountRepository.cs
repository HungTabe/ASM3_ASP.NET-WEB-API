using FUNewsManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNewsManagementSystem.Data.IRepositories
{
    public interface ISystemAccountRepository
    {
        Task<List<SystemAccount>> GetAllAsync();
        Task<SystemAccount> GetByIdAsync(short id);
        Task<SystemAccount> GetByNameOrEmailAsync(string name, string email);
        Task ICreateAsync(SystemAccount account);
        Task IUpdateAsync(SystemAccount account);
        Task<bool> DeleteAsync(short id);
        Task<bool> CanDeleteAsync(short id);
    }
}
