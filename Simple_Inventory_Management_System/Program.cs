namespace Simple_Inventory_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Add a product");
                Console.WriteLine("2. View all products");
                Console.WriteLine("3. Edit a product");
                Console.WriteLine("4. Delete a product");
                Console.WriteLine("5. Search for a product");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct(inventory);
                        break;
                    case "2":
                        ViewAllProducts(inventory);
                        break;
                    case "3":
                        EditProduct(inventory);
                        break;
                    case "4":
                        DeleteProduct(inventory);
                        break;
                    case "5":
                        SearchProduct(inventory);
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddProduct(Inventory inventory)
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter product price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter product quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            try
            {
                Product product = new Product(name, price, quantity);
                var newProduct = inventory.AddProduct(product);
                Console.WriteLine("Product added successfully with ID: " + newProduct.Id);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ViewAllProducts(Inventory inventory)
        {
            var products = inventory.GetProducts();

            if (!products.Any())
            {
                Console.WriteLine("No products found.");
                return;
            }

            products.ForEach(product => Console.WriteLine(product));
        }

        static void EditProduct(Inventory inventory)
        {
            Console.Write("Enter product name: ");
            string productName = Console.ReadLine();

            Product? product = inventory.GetProductByName(productName);
            if (product is null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.WriteLine("Your product is: " + product);

            Console.Write("Enter new product name (or press Enter to keep the current name): ");
            string name = Console.ReadLine();
            string newName = String.IsNullOrEmpty(name) ? product.Name : name;

            Console.Write("Enter new product price (or press Enter to keep the current price): ");
            string price = Console.ReadLine();
            decimal newPrice = String.IsNullOrEmpty(price) ? product.Price : Convert.ToDecimal(price);

            Console.Write("Enter new product quantity (or press Enter to keep the current quantity): ");
            string quantity = Console.ReadLine();
            int newQuantity = String.IsNullOrEmpty(quantity) ? product.Quantity : Convert.ToInt32(quantity);
            try 
            {
                var updatedProduct = new Product(newName, newPrice, newQuantity);
                updatedProduct.Id = product.Id;
                Console.WriteLine(inventory.UpdateProduct(updatedProduct)
                    ? "Product updated successfully."
                    : "An error occurred while updating the product. Please try again.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        static void DeleteProduct(Inventory inventory)
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.WriteLine(inventory.DeleteProduct(name)
                ? "Product deleted successfully."
                : "Product not found. Please try again.");
        }

        static void SearchProduct(Inventory inventory)
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Product? product = inventory.SearchProduct(name);
            if (product is null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.WriteLine(product);
        }
    }
}