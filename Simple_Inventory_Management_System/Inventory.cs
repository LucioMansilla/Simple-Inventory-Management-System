using Simple_Inventory_Management_System.DataBase;

namespace Simple_Inventory_Management_System;

public class Inventory
{
    private readonly DatabaseContext _ctx;

    public Inventory(DatabaseContext ctx)
    {
        _ctx = ctx;
    }

    public Product AddProduct(Product product)
    {
        if (_ctx.GetProductByName(product.Name) != null)
            throw new Exception("Product with the same name already exists");

        var addedProduct = _ctx.AddProduct(product);
        return addedProduct;
    }

    public List<Product> GetProducts()
    {
        return _ctx.GetProducts();
    }

    public Product GetProductByName(string name)
    {
        return _ctx.GetProductByName(name);
    }

    public bool UpdateProduct(Product updatedProduct)
    {
        var product = _ctx.GetProductById(updatedProduct.Id);
        if (product == null) return false;

        if (_ctx.GetProducts().Any(p =>
                p.Id != updatedProduct.Id && p.Name.Equals(updatedProduct.Name, StringComparison.OrdinalIgnoreCase)))
            return false;

        return _ctx.UpdateProduct(updatedProduct);
    }

    public bool DeleteProduct(string name)
    {
        var product = _ctx.GetProductByName(name);
        if (product == null) return false;
        return _ctx.DeleteProduct(name);
    }

    public Product? SearchProduct(string name)
    {
        return _ctx.GetProductByName(name);
    }
}