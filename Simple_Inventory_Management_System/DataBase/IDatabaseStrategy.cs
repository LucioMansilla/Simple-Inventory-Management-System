namespace Simple_Inventory_Management_System.DataBase;

public interface IDatabaseStrategy
{
    Product Insert(Product product);
    List<Product> GetProducts();
    Product? GetProductByName(string name);
    Product? GetProductById(int id);
    bool UpdateProduct(Product product);
    bool DeleteProduct(string name);
}