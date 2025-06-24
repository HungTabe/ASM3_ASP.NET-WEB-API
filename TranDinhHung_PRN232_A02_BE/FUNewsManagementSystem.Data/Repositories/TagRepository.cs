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
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(FunewsManagementA2Context context) : base(context) { }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task CreateAsync(Tag tag)
        {
            await CreateAsync(tag);
        }

        public async Task UpdateAsync(Tag tag)
        {
            await UpdateAsync(tag);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tag = await GetByIdAsync(id);
            if (tag == null) return false;
            return await RemoveAsync(tag);
        }
    }
}
