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

    public Product AddProduct(Product product)
    {
        product.Id = _nextProductId++;
        _products.Add(product);
        return product;
    }

    public List<Product> GetProducts() => _products;

    public Product? GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public bool UpdateProduct(Product updatedProduct)
    {
        var product = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
        if (product == null) return false;
        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        product.Quantity = updatedProduct.Quantity;
        return true;
    }
    
    
    public bool DeleteProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return false;
        _products.Remove(product);
        return true;
    }
    
 

}
