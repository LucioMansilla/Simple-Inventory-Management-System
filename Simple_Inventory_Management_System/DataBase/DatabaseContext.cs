namespace Simple_Inventory_Management_System.DataBase;

public class DatabaseContext
{
    private IDatabaseStrategy _databaseStrategy;

    public DatabaseContext(IDatabaseStrategy databaseStrategy)
    {
        _databaseStrategy = databaseStrategy;
    }

    public void SetDatabaseStrategy(IDatabaseStrategy databaseStrategy)
    {
        _databaseStrategy = databaseStrategy;
    }

    public IDatabaseStrategy GetDatabaseStrategy()
    {
        return _databaseStrategy;
    }

    public Product AddProduct(Product product)
    {
        return _databaseStrategy.Insert(product);
    }

    public List<Product> GetProducts()
    {
        return _databaseStrategy.GetProducts();
    }

    public Product? GetProductByName(string name)
    {
        return _databaseStrategy.GetProductByName(name);
    }

    public bool UpdateProduct(Product product)
    {
        return _databaseStrategy.UpdateProduct(product);
    }

    public bool DeleteProduct(string name)
    {
        return _databaseStrategy.DeleteProduct(name);
    }

    public Product? GetProductById(int id)
    {
        return _databaseStrategy.GetProductById(id);
    }
}