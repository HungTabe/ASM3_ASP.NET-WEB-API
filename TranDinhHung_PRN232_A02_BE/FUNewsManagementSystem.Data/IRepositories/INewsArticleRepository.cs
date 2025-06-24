using FUNewsManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNewsManagementSystem.Data.IRepositories
{
    public interface INewsArticleRepository
    {
        Task<List<NewsArticle>> GetAllAsync();
        Task<NewsArticle> GetByIdAsync(string id);
        Task CreateAsync(NewsArticle article);
        Task UpdateAsync(NewsArticle article);
        Task<bool> DeleteAsync(string id);
    }
}
