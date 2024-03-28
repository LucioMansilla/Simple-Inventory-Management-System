using System.Runtime.CompilerServices;
using Simple_Inventory_Management_System;

namespace Simple_Inventory_Management_System_Tests;

public class ProductTests
{
    [Theory]
    [InlineData("Product", 10.5, 10)]
    public void ToString_ShouldReturnCorrectFormat_WhenCalled(string name, decimal price, int quantity)
    {
        // Arrange
        var product = new Product(name, price, quantity);

        // Act
        var productString = product.ToString();

        // Assert
        var expectedString = $"ID: {product.Id}, Name: {name}, Price: {price:C}, Quantity: {quantity}";
        Assert.Equal(expectedString, productString);
    }

    [Theory]
    [InlineData("Product", 10.5, 10)]
    public void Equals_ShouldReturnTrue_WhenProductsAreEqual(string name, decimal price, int quantity)
    {
        // Arrange
        var product1 = new Product(name, price, quantity);
        var product2 = new Product(name, price, quantity);

        // Act
        var result = product1.Equals(product2);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("Product1", 10.5, 10, "Product2", 20.5, 20)]
    public void Equals_ShouldReturnFalse_WhenProductsAreNotEqual(string name1, decimal price1, int quantity1,
        string name2, decimal price2, int quantity2)
    {
        // Arrange
        var product1 = new Product(name1, price1, quantity1);
        var product2 = new Product(name2, price2, quantity2);

        // Act
        var result = product1.Equals(product2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Constructor_ShouldCreateProduct_WhenGivenValidArguments()
    {
        // Act
        var product = new Product("Test", 10, 5);

        // Assert
        Assert.Equal("Test", product.Name);
        Assert.Equal(10, product.Price);
        Assert.Equal(5, product.Quantity);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_ShouldThrowArgumentException_WhenNameIsInvalid(string invalidName)
    {
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new Product(invalidName, 10, 5));

        // Assert
        Assert.Equal("Name cannot be null or empty.", exception.Message);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_ShouldThrowArgumentException_WhenPriceIsInvalid(decimal invalidPrice)
    {
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new Product("Test", invalidPrice, 5));

        // Assert
        Assert.Equal("Price cannot be negative.", exception.Message);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-5)]
    public void Constructor_ShouldThrowArgumentException_WhenQuantityIsInvalid(int invalidQuantity)
    {
        // Act 
        var exception = Assert.Throws<ArgumentException>(() => new Product("Test", 10m, invalidQuantity));

        // Assert
        Assert.Equal("Quantity cannot be negative.", exception.Message);
    }
    
    [Fact]
    public void Equals_ShouldReturnFalse_WhenObjectIsNull()
    {
        // Arrange
        var product = new Product("Test", 10, 5);

        // Act
        var result = product.Equals(null);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void Equals_ShouldReturnFalse_WhenObjectIsNotProduct()
    {
        // Arrange
        var product = new Product("Test", 10, 5);

        // Act
        var result = product.Equals("Test");

        // Assert
        Assert.False(result);
    }
    
    [Theory]
    [InlineData("Test", 10, 5)]
    [InlineData("Test", 20, 5)]
    public void GetHashCode_ShouldReturnSameHashCode_WhenProductsAreEqual(string name, decimal price, int quantity)
    {
        // Arrange
        var product1 = new Product(name, price, quantity);
        var product2 = new Product(name, price, quantity);

        // Act
        var hashCode1 = product1.GetHashCode();
        var hashCode2 = product2.GetHashCode();

        // Assert
        Assert.Equal(hashCode1, hashCode2);
    }
}