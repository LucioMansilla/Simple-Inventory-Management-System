using Simple_Inventory_Management_System;

namespace Simple_Inventory_Management_System_Tests;

public class InventoryTests
{
    [Fact]
    public void AddProduct_ShouldAddProduct_WhenNewName()
    {
        // Arrange
        var inventory = new Inventory();
        var product = new Product("Test", 10, 5);

        // Act
        var addedProduct = inventory.AddProduct(product);

        // Assert
        Assert.Contains(addedProduct, inventory.GetProducts());
    }

    [Fact]
    public void AddProduct_ShouldThrow_WhenDuplicateName()
    {
        // Arrange
        var inventory = new Inventory();
        var product = new Product("Test", 10, 5);
        inventory.AddProduct(product);

        // Act
        var exception = Assert.Throws<Exception>(() => inventory.AddProduct(new Product("Test", 20, 10)));
        
        // Assert
        Assert.Equal("Product with the same name already exists", exception.Message);
    }

    [Theory]
    [InlineData("TestProduct1")]
    [InlineData("TestProduct2")]
    [InlineData("TestProduct3")]
    public void DeleteProduct_ShouldReturnTrue_WhenProductIsDeleted(string productName)
    {
        // Arrange
        var inventory = new Inventory();
        inventory.AddProduct(new Product(productName, 10, 5));

        // Act
        bool result = inventory.DeleteProduct(productName);

        // Assert
        Assert.True(result);
        Assert.Null(inventory.GetProductByName(productName));
    }

    [Theory]
    [InlineData("TestProduct1")]
    [InlineData("TestProduct2")]
    [InlineData("TestProduct3")]
    public void DeleteProduct_ShouldReturnFalse_WhenProductDoesNotExist(string productName)
    {
        // Arrange
        var inventory = new Inventory();

        // Act
        bool result = inventory.DeleteProduct(productName);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetProductByName_ShouldRetrieveCorrectProduct_WhenProductExists()
    {
        // Arrange
        var inventory = new Inventory();
        var product = new Product("Test", 10, 5);
        inventory.AddProduct(product);

        // Act
        var retrievedProduct = inventory.GetProductByName("Test");

        // Assert
        Assert.Equal(product, retrievedProduct);
    }

    [Fact]
    public void GetProductByName_ShouldReturnNull_WhenProductDoesNotExist()
    {
        // Arrange
        var inventory = new Inventory();

        // Act
        var retrievedProduct = inventory.GetProductByName("NonExisting");

        // Assert
        Assert.Null(retrievedProduct);
    }
    
    [Fact]
    public void UpdateProduct_ShouldUpdateProduct_WhenProductExists()
    {
        // Arrange
        var inventory = new Inventory();
        var product = new Product("Test", 10, 5);
        inventory.AddProduct(product);
        var updatedProduct = new Product("Test", 20, 10) { Id = product.Id };

        // Act
        bool result = inventory.UpdateProduct(updatedProduct);

        // Assert
        Assert.True(result);
        Assert.Equal(updatedProduct, inventory.GetProductByName("Test"));
    }
    
    [Fact]
    public void UpdateProduct_ShouldReturnFalse_WhenProductDoesNotExist()
    {
        // Arrange
        var inventory = new Inventory();
        var product = new Product("Test", 10, 5);

        // Act
        bool result = inventory.UpdateProduct(product);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void UpdateProduct_ShouldReturnFalse_WhenDuplicateName()
    {
        // Arrange
        var inventory = new Inventory();
        var product1 = new Product("Test1", 10, 5);
        var product2 = new Product("Test2", 20, 10);
        inventory.AddProduct(product1);
        inventory.AddProduct(product2);

        // Act
        product1.Name = "Test2";
        bool result = inventory.UpdateProduct(product1);

        // Assert
        Assert.False(result);
    }
    
    [Theory]
    [InlineData("TestProduct1")]
    [InlineData("TestProduct2")]
    [InlineData("TestProduct3")]
    public void SearchProduct_ShouldReturnProduct_WhenProductExists(string productName)
    {
        // Arrange
        var inventory = new Inventory();
        var product = new Product(productName, 10, 5);
        inventory.AddProduct(product);

        // Act
        var result = inventory.SearchProduct(productName);

        // Assert
        Assert.Equal(product, result);
    }  
    
    [Theory]
    [InlineData("TestProduct1")]
    [InlineData("TestProduct2")]
    [InlineData("TestProduct3")]
    public void SearchProduct_ShouldReturnNull_WhenProductDoesNotExist(string productName)
    {
        // Arrange
        var inventory = new Inventory();

        // Act
        var result = inventory.SearchProduct(productName);

        // Assert
        Assert.Null(result);
    }
    
    
 
}