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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(FunewsManagementA2Context context) : base(context) { }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(short id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task CreateAsync(Category category)
        {
            await CreateAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            await UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(short id)
        {
            var category = await GetByIdAsync(id);
            if (category == null || !await CanDeleteAsync(id)) return false;
            return await RemoveAsync(category);
        }

        public async Task<bool> CanDeleteAsync(short id)
        {
            return !await _context.NewsArticles.AnyAsync(n => n.CategoryId == id);
        }
    }
}
