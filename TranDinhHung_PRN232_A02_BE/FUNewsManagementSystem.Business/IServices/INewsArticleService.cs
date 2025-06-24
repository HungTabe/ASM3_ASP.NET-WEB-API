using FUNewsManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNewsManagementSystem.Business.IServices
{
    public interface INewsArticleService
    {
        Task<List<NewsArticle>> GetAllAsync();
        Task<NewsArticle> GetByIdAsync(string id);
        Task CreateAsync(NewsArticle article, List<int> tagIds);
        Task UpdateAsync(NewsArticle article, List<int> tagIds);
        Task<bool> DeleteAsync(string id);
    }
}
