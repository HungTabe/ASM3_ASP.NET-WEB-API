using FUNewsManagementSystem.Business.IServices;
using FUNewsManagementSystem.Data.Entities;
using FUNewsManagementSystem.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNewsManagementSystem.Business.Services
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _newsArticleRepository;
        private readonly ITagRepository _tagRepository;

        public NewsArticleService(INewsArticleRepository newsArticleRepository, ITagRepository tagRepository)
        {
            _newsArticleRepository = newsArticleRepository;
            _tagRepository = tagRepository;
        }

        public async Task<List<NewsArticle>> GetAllAsync()
        {
            return await _newsArticleRepository.GetAllAsync();
        }

        public async Task<NewsArticle> GetByIdAsync(string id)
        {
            return await _newsArticleRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(NewsArticle article, List<int> tagIds)
        {
            await _newsArticleRepository.CreateAsync(article);
            if (tagIds != null && tagIds.Any())
            {
                await _newsArticleRepository.AddTagsAsync(article.NewsArticleId, tagIds);
            }
        }

        public async Task UpdateAsync(NewsArticle article, List<int> tagIds)
        {
            await _newsArticleRepository.UpdateAsync(article);
            await _newsArticleRepository.RemoveTagsAsync(article.NewsArticleId);
            if (tagIds != null && tagIds.Any())
            {
                await _newsArticleRepository.AddTagsAsync(article.NewsArticleId, tagIds);
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            await _newsArticleRepository.RemoveTagsAsync(id); // Xóa Tags trước
            return await _newsArticleRepository.DeleteAsync(id);
        }
    }
}
