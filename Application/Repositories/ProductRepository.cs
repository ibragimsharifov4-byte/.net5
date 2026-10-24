using Application.Data;
using Domain.Interfaces;
using Domain.Models;
using Npgsql;

namespace Application.Repositories;

public class ProductRepository : IProductRepository
{
    public void AddProduct(Product product)
    {
        using var con = Database.Connection();
        con.Open();
        var query = "insert into products(name , price , category_id) values (@name , @price , @categoryId)";
        var cmd = new NpgsqlCommand(query , con);
        cmd.Parameters.AddWithValue("name" , product.Name);
        cmd.Parameters.AddWithValue("price" , product.Price);
        cmd.Parameters.AddWithValue("categoryId" , product.CategoryId);
        cmd.ExecuteNonQuery();
    }

    public void DeleteProduct(int id)
    {
        using var con = Database.Connection();
        con.Open();
        var query = "delete from products where id = @id";
        var cmd = new NpgsqlCommand(query , con);
        cmd.Parameters.AddWithValue("id" , id);
        cmd.ExecuteNonQuery();
    }

    public List<Product> GetCategories()
    {
        using var con = Database.Connection();
        con.Open();
        var query = "select id , name , price , category_id from products";
        var cmd = new NpgsqlCommand(query , con);
        var reader = cmd.ExecuteReader();
        List<Product> products = [];
        while(reader.Read())
        {
            var product = new Product()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2),
                CategoryId = reader.GetInt32(3)
            };
            products.Add(product);
        }
        return products;  
    }

    public Product GetProduct(int id)
    {
        using var con = Database.Connection();
        con.Open();
        var query = "select id , name , price , category_id from products where id = @id";
        var cmd = new NpgsqlCommand(query , con);
        cmd.Parameters.AddWithValue("id" , id);
        var product = new Product();
        var data = cmd.ExecuteReader();
        while(data.Read())
        {
            product.Id = data.GetInt32(0);
            product.Name = data.GetString(1);
            product.Price = data.GetDecimal(2);
            product.CategoryId = data.GetInt32(3);
            return product;
        }
        return null;
    }

    public List<Product> GetProductsByCategory(string categoryName)
    {
        var products = new List<Product>();
        using var conn = Database.Connection();
        conn.Open();
        var query = @"
        SELECT p.id, p.name, p.price
        FROM products p
        JOIN categories c ON p.category_id = c.id
        WHERE c.name = @categoryName";
        var com = new NpgsqlCommand(query, conn);
        com.Parameters.AddWithValue("@categoryName", categoryName);
        var reader = com.ExecuteReader();
        while (reader.Read())
        {
            var product = new Product()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2)
            };
            products.Add(product);
        }
        return products;
    }

    public void UpdateProduct(Product product)
    {
        using var con = Database.Connection();
        con.Open();
        var query = @"UPDATE products
        set name = @name,
        price = @price,
        category_id = @categoryId
        where id = @id";
        var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("@id", product.Id);
        cmd.Parameters.AddWithValue("@name", product.Name);
        cmd.Parameters.AddWithValue("@price", product.Price);
        cmd.Parameters.AddWithValue("@categoryId", product.CategoryId);
        cmd.ExecuteNonQuery();
    }
}
