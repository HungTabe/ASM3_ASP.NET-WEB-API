using FUNewsManagementSystem.Data.Basic;
using FUNewsManagementSystem.Data.Data;
using FUNewsManagementSystem.Data.Entities;
using FUNewsManagementSystem.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNewsManagementSystem.Data.Repositories
{
    public class SystemAccountRepository : GenericRepository<SystemAccount>, ISystemAccountRepository
    {
        public SystemAccountRepository(FunewsManagementA2Context context) : base(context) { }

        public async Task<List<SystemAccount>> GetAllAsync()
        {
            return await _context.SystemAccounts.ToListAsync();
        }

        public async Task<SystemAccount> GetByIdAsync(short id)
        {
            return await _context.SystemAccounts.FindAsync(id);
        }

        public async Task<SystemAccount> GetByNameOrEmailAsync(string name, string email)
        {
            return await _context.SystemAccounts.FirstOrDefaultAsync(r => r.AccountName == name || r.AccountEmail == email);
        }

        public async Task ICreateAsync(SystemAccount account)
        {
            await CreateAsync(account);
        }

        public async Task IUpdateAsync(SystemAccount account)
        {
            await UpdateAsync(account);
        }

        public async Task<bool> DeleteAsync(short id)
        {
            var account = await GetByIdAsync(id);
            if (account == null || !await CanDeleteAsync(id)) return false;
            return await RemoveAsync(account);
        }

        public async Task<bool> CanDeleteAsync(short id)
        {
            return !await _context.NewsArticles.AnyAsync(n => n.CreatedById == id || n.UpdatedById == id);
        }
    }
}
