using Domain.Models;

namespace Domain.Interfaces;

public interface IProductRepository
{
    void AddProduct(Product product);
    Product GetProduct(int id);
    List<Product> GetProducts();
    List<Product> GetProductsByCategory(string categoryName);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
}
