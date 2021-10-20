using System.Collections.Generic;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public interface ICategoryService
    {
        void AddCategory(CategoryDTO categoryDto);
        void UpdateCategory(CategoryDTO categoryDto);
        CategoryDTO GetCategoryById(int categoryId);
        void RemoveCategory(int categoryId);
        IEnumerable<CategoryDTO> GetAllCategories();
    }
}