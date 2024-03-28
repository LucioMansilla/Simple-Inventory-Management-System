namespace Simple_Inventory_Management_System;

public class Product
{
    private decimal _price;
    private int _quantity;
    private string _name;
    public int Id { get; set; }
    
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be null or empty.");
            _name = value;
        }
    }
    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative.");
            _price = value;
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value < 0)
                throw new ArgumentException("Quantity cannot be negative.");
            _quantity = value;
        }
    }

    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Price: {Price:C}, Quantity: {Quantity}";
    }
    
    public override bool Equals(object? obj)
    {
        if(obj is null)
            return false;
        if(ReferenceEquals(this, obj))
            return true;
        if (obj is not Product product)
            return false;
        return Id == product.Id && Name == product.Name && Price == product.Price && Quantity == product.Quantity;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Price, Quantity);
    }
    
    
    
}