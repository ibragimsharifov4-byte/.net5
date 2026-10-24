using Domain.Models;

namespace Domain.Interfaces;

public interface ICategoryRepository
{
    void AddCategory(Category category);
    Category GetCategory(int id);
    List<Category> GetCategories();
    void UpdateCategory(Category category);
    void DeleteCategory(int id);
}
