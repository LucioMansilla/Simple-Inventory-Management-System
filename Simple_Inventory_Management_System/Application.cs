using Simple_Inventory_Management_System.DataBase;

namespace Simple_Inventory_Management_System;

public class Application
{
    private readonly DatabaseContext _context;
    private readonly Inventory _inventory;
    private readonly MongoDbStrategy _mongoDbStrategy;
    private readonly SqlServerStrategy _sqlServerStrategy;

    public Application(Inventory inventory, DatabaseContext context, SqlServerStrategy sqlServerStrategy,
        MongoDbStrategy mongoDbStrategy)
    {
        _inventory = inventory;
        _context = context;
        _sqlServerStrategy = sqlServerStrategy;
        _mongoDbStrategy = mongoDbStrategy;
    }

    public void Run()
    {
        var running = true;
        while (running)
        {
            Console.WriteLine(
                $"Current database: {(_context.GetDatabaseStrategy() is SqlServerStrategy ? "SQL Server" : "MongoDB")}");
            Console.WriteLine("1. Add a product");
            Console.WriteLine("2. View all products");
            Console.WriteLine("3. Edit a product");
            Console.WriteLine("4. Delete a product");
            Console.WriteLine("5. Search for a product");
            Console.WriteLine("6. Switch database");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    ViewAllProducts();
                    break;
                case "3":
                    EditProduct();
                    break;
                case "4":
                    DeleteProduct();
                    break;
                case "5":
                    SearchProduct();
                    break;
                case "6":
                    SwitchDatabase();
                    break;
                case "7":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void AddProduct()
    {
        Console.Write("Enter product name: ");
        var name = Console.ReadLine();

        Console.Write("Enter product price: ");
        var price = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Enter product quantity: ");
        var quantity = Convert.ToInt32(Console.ReadLine());
        try
        {
            var product = new Product(name, price, quantity);
            var newProduct = _inventory.AddProduct(product);
            Console.WriteLine("Product added successfully with ID: " + newProduct.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void ViewAllProducts()
    {
        var products = _inventory.GetProducts();

        if (!products.Any())
        {
            Console.WriteLine("No products found.");
            return;
        }

        products.ForEach(product => Console.WriteLine(product));
    }

    private void EditProduct()
    {
        Console.Write("Enter product name: ");
        var productName = Console.ReadLine();

        var product = _inventory.GetProductByName(productName);
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.WriteLine("Your product is: " + product);

        Console.Write("Enter new product name (or press Enter to keep the current name): ");
        var name = Console.ReadLine();
        var newName = string.IsNullOrEmpty(name) ? product.Name : name;

        Console.Write("Enter new product price (or press Enter to keep the current price): ");
        var price = Console.ReadLine();
        var newPrice = string.IsNullOrEmpty(price) ? product.Price : Convert.ToDecimal(price);

        Console.Write("Enter new product quantity (or press Enter to keep the current quantity): ");
        var quantity = Console.ReadLine();
        var newQuantity = string.IsNullOrEmpty(quantity) ? product.Quantity : Convert.ToInt32(quantity);
        try
        {
            var updatedProduct = new Product(newName, newPrice, newQuantity) { Id = product.Id };
            Console.WriteLine(_inventory.UpdateProduct(updatedProduct)
                ? "Product updated successfully."
                : "An error occurred while updating the product. Please try again.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void DeleteProduct()
    {
        Console.Write("Enter product name: ");
        var name = Console.ReadLine();

        Console.WriteLine(_inventory.DeleteProduct(name)
            ? "Product deleted successfully."
            : "Product not found. Please try again.");
    }

    private void SearchProduct()
    {
        Console.Write("Enter product name: ");
        var name = Console.ReadLine();

        var product = _inventory.SearchProduct(name);
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.WriteLine(product);
    }

    private void SwitchDatabase()
    {
        if (_context.GetDatabaseStrategy() is SqlServerStrategy)
            _context.SetDatabaseStrategy(_mongoDbStrategy);
        else
            _context.SetDatabaseStrategy(_sqlServerStrategy);
        Console.WriteLine(
            $"Switched to: {(_context.GetDatabaseStrategy() is SqlServerStrategy ? "SQL Server" : "MongoDB")}");
    }
}