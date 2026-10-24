using Domain.Models;
using Application.Repositories;

var categoryRepo = new CategoryRepository();
var productRepo = new ProductRepository();

while (true)
{
    System.Console.WriteLine("===============================");
    System.Console.WriteLine(@"1 - Show all categories.
2 - Show category.
3 - Add new category.
4 - Update category.
5 - Delete category.
6 - Get products.
7 - Get product by id
8 - Add product
9 - Update product
10 - Delete product
11 - Get products by category
0 - Exit.");
    var a = Console.ReadLine();
    System.Console.WriteLine("===============================");
    switch (a)
    {
        case "1":
            var allCategories = categoryRepo.GetCategories();
            foreach(var item in allCategories)
            {
                System.Console.WriteLine(item.Id+" "+item.Name);
            }
            break;

        case "2":
            System.Console.Write("Enter id of category: ");
            var categoryIdToShow = Convert.ToInt32(Console.ReadLine());
            var categoryToShow = categoryRepo.GetCategory(categoryIdToShow);
            System.Console.WriteLine(categoryToShow.Id+" "+categoryToShow.Name);
            break;

        case "3":
            System.Console.Write("Enter name: ");
            var categoryName = Console.ReadLine()!;
            var newCategory = new Category()
            {
                Name = categoryName
            };
            categoryRepo.AddCategory(newCategory);
            break;

        case "4":
            System.Console.Write("Enter id: ");
            var categoryIdToUpdate = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter new name: ");
            var newCategoryName = Console.ReadLine()!;
            var categoryToUpdate = new Category()
            {
                Id = categoryIdToUpdate,
                Name = newCategoryName
            };
            categoryRepo.UpdateCategory(categoryToUpdate);
            break;

        case "5":
            System.Console.Write("Enter id: ");
            var categoryIdToDelete = Convert.ToInt32(Console.ReadLine());
            categoryRepo.DeleteCategory(categoryIdToDelete);
            break;

        case "6":
        var products = productRepo.GetCategories();
        foreach (var item in products)
        {
            Console.WriteLine($"{item.Id} - {item.Name} - {item.Price} - Category ID: {item.CategoryId}");
        }
        break;

        case "7":
        Console.Write("Enter product id: ");
        var productId = Convert.ToInt32(Console.ReadLine());
        var product = productRepo.GetProduct(productId);
        if (product != null)
        {
            Console.WriteLine($"{product.Id} - {product.Name} - {product.Price} - Category ID: {product.CategoryId}");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
        break;

        case "8":
        Console.Write("Enter product name: ");
        var productName = Console.ReadLine();
        Console.Write("Enter price: ");
        var price = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Enter category id: ");
        var newProductCategoryId = Convert.ToInt32(Console.ReadLine());
        var produc =  new Product()
        {
            Name = productName,
            Price = price,
            CategoryId = newProductCategoryId
        };
        productRepo.AddProduct(produc);
        Console.WriteLine("Product added.");
        break;

        case "9":
        Console.Write("Enter product id: ");
        var updateProductId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter new name: ");
        var newProductName = Console.ReadLine()!;
        Console.Write("Enter new price: ");
        var newPrice = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Enter new category id: ");
        var newProductCategoryId2 = Convert.ToInt32(Console.ReadLine());
        var produc1 = new Product()
        {
            Id = updateProductId,
            Name = newProductName,
            Price = newPrice,
            CategoryId = newProductCategoryId2
        };
        productRepo.UpdateProduct(produc1);
        Console.WriteLine("Product updated.");
        break;

        case "10":
        Console.Write("Enter product id: ");
        var deleteProductId = Convert.ToInt32(Console.ReadLine());
        productRepo.DeleteProduct(deleteProductId);
        Console.WriteLine("Product deleted.");
        break;

        case "11":
        Console.Write("Enter category name: ");
        var searchCategoryName = Console.ReadLine()!;
        var categoryProducts =
        productRepo.GetProductsByCategory(searchCategoryName);
        if (categoryProducts.Count == 0)
        {
            Console.WriteLine("No products found in this category.");
        }
        foreach (var item in categoryProducts)
        {
            Console.WriteLine(
                $"{item.Id} - {item.Name} - {item.Price}");
        }
        break;

        case "0":
            System.Console.WriteLine("Exiting ...");
            return;
    }
}