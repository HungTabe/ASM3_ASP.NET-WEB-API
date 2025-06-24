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
    public class NewsArticleRepository : GenericRepository<NewsArticle>, INewsArticleRepository
    {
        public NewsArticleRepository(FunewsManagementA2Context context) : base(context) { }

        public async Task<List<NewsArticle>> GetAllAsync()
        {
            return await _context.NewsArticles.ToListAsync();
        }

        public async Task<NewsArticle> GetByIdAsync(string id)
        {
            return await _context.NewsArticles.FindAsync(id);
        }

        public async Task CreateAsync(NewsArticle article)
        {
            await CreateAsync(article);
        }

        public async Task UpdateAsync(NewsArticle article)
        {
            await UpdateAsync(article);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var article = await GetByIdAsync(id);
            if (article == null) return false;
            return await RemoveAsync(article);
        }
    }
}
