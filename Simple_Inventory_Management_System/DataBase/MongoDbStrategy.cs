using MongoDB.Bson;
using MongoDB.Driver;

namespace Simple_Inventory_Management_System.DataBase;

public class MongoDbStrategy : IDatabaseStrategy
{
    private readonly IMongoCollection<Product> _products;

    public MongoDbStrategy(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("SimpleInventoryDB");
        _products = database.GetCollection<Product>("Products");
    }

    public Product Insert(Product product)
    {
        product.Id = (int)_products.CountDocuments(new BsonDocument());
        _products.InsertOne(product);
        return product;
    }


    public List<Product> GetProducts()
    {
        return _products.Find(p => true).ToList();
    }

    public Product? GetProductById(int id)
    {
        return null;
    }

    public Product? GetProductByName(string name)
    {
        return _products.Find(p => p.Name == name).FirstOrDefault();
    }

    public bool UpdateProduct(Product product)
    {
        var result = _products.ReplaceOne(p => p.Id == product.Id, product);
        return result.ModifiedCount > 0;
    }

    public bool DeleteProduct(string name)
    {
        var result = _products.DeleteOne(p => p.Name == name);
        return result.DeletedCount > 0;
    }
}