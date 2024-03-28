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
        if (_products.Any(p => p.Name == product.Name))
            throw new Exception("Product with the same name already exists");

        product.Id = _nextProductId++;
        _products.Add(product);
        return product;
    }

    public List<Product> GetProducts() => _products;

    public Product? GetProductByName(string name) => _products.FirstOrDefault(p => p.Name == name);

    public bool UpdateProduct(Product updatedProduct)
    {
        var product = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
        if (product == null) return false;

        if (_products.Any(p =>
                p.Id != updatedProduct.Id && p.Name.Equals(updatedProduct.Name, StringComparison.OrdinalIgnoreCase)))
            return false;

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        product.Quantity = updatedProduct.Quantity;

        return true;
    }

    public bool DeleteProduct(string name)
    {
        var product = _products.FirstOrDefault(p => p.Name == name);
        if (product == null) return false;
        _products.Remove(product);
        return true;
    }

    public Product? SearchProduct(string name) => _products.FirstOrDefault(p => p.Name == name);
}