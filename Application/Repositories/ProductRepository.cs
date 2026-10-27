using Application.Data;
using Domain.Interfaces;
using Domain.Models;
using Npgsql;
using Dapper;

namespace Application.Repositories;

public class ProductRepository : IProductRepository
{
    public void AddProduct(Product product)
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "insert into products(name , price , category_id) values(@name , @price , @categoryId)";
        con.Execute(sql , new {name = product.Name , price = product.Price , categoryId = product.CategoryId});
    }

    public void DeleteProduct(int id)
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "delete from products where id = @id";
        con.Execute(sql, new { id = id });
    }

    public List<Product> GetProducts()
    {
        using var con = Database.Connection();
        con.Open();
        var query = "select id , name , price , category_id from products";
        var products = con.Query<Product>(query).ToList();
        return products;
    }

    public Product GetProduct(int id)
    {
        using var con = Database.Connection();
        con.Open();
        var query = "select id , name , price , category_id from products where id = @id";
        var product = con.QuerySingleOrDefault<Product>(query, new { id = id });
        return product;
    }

    public List<Product> GetProductsByCategory(string categoryName)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = 
        @"select p.id, p.name, p.price, p.category_id from products p join categories c on p.category_id = c.id where c.name = @categoryName";
        var products = conn.Query<Product>(query , new { categoryName = categoryName }).ToList();
        return products;
    }

    public void UpdateProduct(Product product)
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "update products set name = @name , price = @price , category_id = @categoryId where id = @id";
        con.Execute(sql, new { name = product.Name, price = product.Price, categoryId = product.CategoryId, id = product.Id });
    }

    public void GetAmountOfProducts()
    {
        using var con = Database.Connection();
        con.Open();
        var query = "select count(*) from products";
        var amount = con.ExecuteScalar<int>(query);
        Console.WriteLine($"Total number of products: {amount}");
    }

    public void GetAmountOfProductsByCategory(int categoryId)
    {
        using var con = Database.Connection();
        con.Open();
        var query = "select count(*) from products where category_id = @categoryId";
        var amount = con.ExecuteScalar<int>(query, new { categoryId = categoryId });
        Console.WriteLine($"Total number of products in category {categoryId}: {amount}");
    }

    public decimal GetMinPriceOfProductByCategory(int categoryId)
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "select min(price) from products where category_id = @categoryId";    
        var minPrice = con.ExecuteScalar<decimal>(sql, new { categoryId = categoryId });
        return minPrice;
    }

    public decimal GetMaxPriceOfProductByCategory(int categoryId)
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "select max(price) from products where category_id = @categoryId";
        var maxPrice = con.ExecuteScalar<decimal>(sql, new { categoryId = categoryId });
        return maxPrice;
    }

    public decimal GetAveragePriceOfProductByCategory(int categoryId)
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "select avg(price) from products where category_id = @categoryId";
        var averagePrice = con.ExecuteScalar<decimal>(sql, new { categoryId = categoryId });
        return averagePrice;
    }
}
