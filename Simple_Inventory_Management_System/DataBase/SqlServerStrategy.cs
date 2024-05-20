using System.Data.SqlClient;

namespace Simple_Inventory_Management_System.DataBase;

public class SqlServerStrategy : IDatabaseStrategy
{
    private readonly string _connectionString;

    public SqlServerStrategy(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Product Insert(Product product)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand(
                "INSERT INTO Products (Name, Price, Quantity) OUTPUT INSERTED.Id VALUES (@Name, @Price, @Quantity)",
                connection);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Quantity", product.Quantity);
            product.Id = (int)command.ExecuteScalar();
        }

        return product;
    }

    public List<Product> GetProducts()
    {
        var products = new List<Product>();
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Products", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Product(reader["Name"].ToString(), (decimal)reader["Price"],
                        (int)reader["Quantity"]);
                    product.Id = (int)reader["Id"];
                    products.Add(product);
                }
            }
        }

        return products;
    }

    public Product? GetProductById(int id)
    {
        Product? product = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Products WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    product = new Product(reader["Name"].ToString(), (decimal)reader["Price"], (int)reader["Quantity"]);
                    product.Id = (int)reader["Id"];
                }
            }
        }

        return product;
    }

    public Product? GetProductByName(string name)
    {
        Product? product = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Products WHERE Name = @Name", connection);
            command.Parameters.AddWithValue("@Name", name);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    product = new Product(reader["Name"].ToString(), (decimal)reader["Price"], (int)reader["Quantity"]);
                    product.Id = (int)reader["Id"];
                }
            }
        }

        return product;
    }

    public bool UpdateProduct(Product product)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand(
                "UPDATE Products SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE Id = @Id",
                connection);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Quantity", product.Quantity);
            command.Parameters.AddWithValue("@Id", product.Id);
            return command.ExecuteNonQuery() > 0;
        }
    }

    public bool DeleteProduct(string name)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand("DELETE FROM Products WHERE Name = @Name", connection);
            command.Parameters.AddWithValue("@Name", name);
            return command.ExecuteNonQuery() > 0;
        }
    }
}