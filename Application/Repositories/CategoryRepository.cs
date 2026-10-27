using Application.Data;
using Domain.Interfaces;
using Domain.Models;
using Dapper;

namespace Application.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public void AddCategory(Category category)
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "insert into categories(name) values(@name)";
        con.Execute(sql, new { name = category.Name });
    }

    public void DeleteCategory(int id)
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "delete from categories where id = @id";
        con.Execute(sql, new { id = id });
    }

    public List<Category> GetCategories()
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "select id, name from categories";
        var categories = con.Query<Category>(sql).ToList();
        return categories;
    }

    public Category? GetCategory(int id)
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "select id, name from categories where id = @id";
        var category = con.QuerySingleOrDefault<Category>(sql, new { id = id });
        return category;
    }

    public void UpdateCategory(Category category)
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "update categories set name = @name where id = @id";
        con.Execute(sql, new { id = category.Id, name = category.Name });
    }
    public int GetAmountOfCategories()
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "select count(*) from categories";
        var amount = con.ExecuteScalar<int>(sql);
        return amount;
    }
}