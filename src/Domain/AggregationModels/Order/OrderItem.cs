using Domain.AggregationModels.Book;
using Domain.Root;

namespace Domain.AggregationModels.Order;

public class OrderItem : Entity
{
    private OrderItem() { }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public Book.Book Book { get; set; }
    public OrderItem(int? id, int quantity, int price, Book.Book book)
    {
        Quantity = quantity;
        Price = price;
        Book = book;
        Id = id;
    }
    public static OrderItem Create(int? id, int quantity, int price, Book.Book book)
    {
        if(quantity <= 0)
            throw new Exception("Quantity must be greater than zero");
        
        if(price <= 0) 
            throw new Exception("Price cannot be negative");
        
        return new OrderItem(id, quantity, price, book);
    }
}

public class PurchaseDate : ValueObject
{
    private PurchaseDate() { }
    public DateTime Value { get; }
    public PurchaseDate(DateTime dateTime)
    {
        Value = dateTime;
    }
    public static PurchaseDate Create(DateTime dateTime)
    {
        //validation

        return new PurchaseDate(dateTime);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}