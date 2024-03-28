namespace Simple_Inventory_Management_System;

public class Inventory
{
    private List<Product> _products; 
    private int _nextProductId;

    public Inventory()
    {
        _products = new List<Product>();
        _nextProductId = 1; 
    }
    
    public void AddProduct(Product product)
    {
        product.Id = _nextProductId++; 
        _products.Add(product);
    }
    
    public List<Product> GetProducts() => _products;
}