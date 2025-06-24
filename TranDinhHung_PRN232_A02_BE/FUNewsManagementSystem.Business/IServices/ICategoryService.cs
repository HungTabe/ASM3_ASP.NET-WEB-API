using FUNewsManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNewsManagementSystem.Business.IServices
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(short id);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task<bool> DeleteAsync(short id);
    }
}
