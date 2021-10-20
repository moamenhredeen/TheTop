using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheTop.Application.Dao;
using TheTop.Application.Entities;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private AppDbContext _appDbContext;

        public CategoryService(AppDbContext appDbContext) => _appDbContext = appDbContext;
        
        public void AddCategory(CategoryDTO categoryDto)
        {
            _appDbContext.Categories.Add(new Category() {Name = categoryDto.Name});
            _appDbContext.SaveChanges();
        }

        public void UpdateCategory(CategoryDTO categoryDto)
        {
            _appDbContext.Categories.Update(new Category() {Name = categoryDto.Name, CategoryId = categoryDto.ID});
            _appDbContext.SaveChanges();
        }

        public CategoryDTO GetCategoryById(int categoryId)
        {
            var category = _appDbContext.Categories.Single(c => c.CategoryId == categoryId);
            return new CategoryDTO { Name = category.Name, CreateAt = category.CreatedAt, ID = category.CategoryId };
        }
        
        public void RemoveCategory(int categoryId)
        {
            _appDbContext.Categories.Remove(new Category() {CategoryId = categoryId });
            _appDbContext.SaveChanges();
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categoryList = _appDbContext.Categories.AsNoTracking().ToList();
            return categoryList.Select(category => new CategoryDTO()
            {
                ID = category.CategoryId,
                Name = category.Name,
                CreateAt = category.CreatedAt
            });
        }
    }
}