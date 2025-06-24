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
            return await _context.NewsArticles
                .Include(n => n.Tags)
                .ToListAsync();
        }

        public async Task<NewsArticle> GetByIdAsync(string id)
        {
            return await _context.NewsArticles
                .Include(n => n.Tags)
                .FirstOrDefaultAsync(n => n.NewsArticleId == id);
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

        public async Task AddTagsAsync(string newsArticleId, List<int> tagIds)
        {
            var article = await _context.NewsArticles
                .Include(n => n.Tags)
                .FirstOrDefaultAsync(n => n.NewsArticleId == newsArticleId);
            if (article == null) return;

            var tags = await _context.Tags
                .Where(t => tagIds.Contains(t.TagId))
                .ToListAsync();

            foreach (var tag in tags)
            {
                article.Tags.Add(tag);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveTagsAsync(string newsArticleId)
        {
            var article = await _context.NewsArticles
                .Include(n => n.Tags)
                .FirstOrDefaultAsync(n => n.NewsArticleId == newsArticleId);
            if (article == null) return;

            article.Tags.Clear();
            await _context.SaveChangesAsync();
        }
    }
}
