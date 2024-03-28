/*
 * In this console-based application, you'll create a basic inventory management system. This system will allow a user to manage a list of products, where each product has a name, price, and quantity in stock.
   
   **GitHub requirements:** You must use Git for version control and host your project on GitHub. Commit and push your changes to GitHub at the end of each phase, with meaningful commit messages describing what you have implemented in each commit.
   
   Here are the main functionalities the system should have:
   
   1. **Add a product:** Prompt the user for the product name, price, and quantity, then add the product to the inventory. *Commit the changes and push them to GitHub.*
   2. **View all products:** Display a list of all products in the inventory, along with their prices and quantities. *Commit and push.*
   3. **Edit a product:** Ask the user for a product name. If the product is in the inventory, allow the user to update its name, price, or quantity. *Commit and push.*
   4. **Delete a product:** Ask the user for a product name. If the product is in the inventory, remove it. *Commit and push.*
   5. **Search for a product:** Ask the user for a product name. If the product is in the inventory, display its name, price, and quantity. If not, let the user know the product was not found. *Commit and push.*
   6. **Exit:** Close the application. *Final commit and push.*
   
   For this project, you'll want to create a **`Product`** class with the appropriate properties (name, price, quantity) and methods. You'll also want to create an **`Inventory`** class which maintains a list of **`Product`** objects and provides methods for adding, deleting, and editing products.
 */

using System;
using System.Collections.Generic;

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

            Product product = new Product(name, price, quantity);
            inventory.AddProduct(product);
            Console.WriteLine("Product added successfully.");
        }

    }
}
