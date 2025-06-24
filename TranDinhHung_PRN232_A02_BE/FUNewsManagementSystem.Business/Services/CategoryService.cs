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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllAsync() => await _categoryRepository.GetAllAsync();
        public async Task<Category> GetByIdAsync(short id) => await _categoryRepository.GetByIdAsync(id);
        public async Task CreateAsync(Category category) => await _categoryRepository.CreateAsync(category);
        public async Task UpdateAsync(Category category) => await _categoryRepository.UpdateAsync(category);
        public async Task<bool> DeleteAsync(short id) => await _categoryRepository.DeleteAsync(id);
    }
}
