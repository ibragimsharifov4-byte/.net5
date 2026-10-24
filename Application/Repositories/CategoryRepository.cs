using System.Data.Common;
using Application.Data;
using Domain.Interfaces;
using Domain.Models;
using Npgsql;

namespace Application.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public void AddCategory(Category category)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "insert into categories(name) values(@name)";
        var com = new NpgsqlCommand(query , conn);
        com.Parameters.AddWithValue("name" , category.Name);
        com.ExecuteNonQuery();
    }

    public void DeleteCategory(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "delete from categories where id = @id";
        var com = new NpgsqlCommand(query , conn);
        com.Parameters.AddWithValue("id" , id);
        com.ExecuteNonQuery();
    }

    public List<Category> GetCategories()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select id , name from categories";
        var com = new NpgsqlCommand(query , conn);
        var reader = com.ExecuteReader();
        List<Category> categories = [];
        while(reader.Read())
        {
            var category = new Category()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
            categories.Add(category);
        }

        return categories;
    }

    public Category GetCategory(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select id , name from categories where id = @id";
        var com = new NpgsqlCommand(query , conn);
        com.Parameters.AddWithValue("id" , id);
        var category = new Category();
        var data = com.ExecuteReader();
        while(data.Read())
        {
            category.Id = data.GetInt32(0);
            category.Name = data.GetString(1);
            return category;
        }
        return null;
    }

    public void UpdateCategory(Category category)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "update categories set @id = id , name = @name where id = @id";
        var com = new NpgsqlCommand(query , conn);
        com.Parameters.AddWithValue("id" , category.Id);
        com.Parameters.AddWithValue("name" , category.Name);
        com.ExecuteNonQuery();
    }
}
