using Domain.Root;

namespace Domain.AggregationModels.Order;

public class Order : Entity
{
    private Order() { }

    public Order(int? id, PurchaseDate purchaseDate, List<OrderItem> orderItems)
    {
        Id = id;
        OrderItems = orderItems;
        DateTime = purchaseDate;
    }
    public List<OrderItem> OrderItems { get; set; }
    public PurchaseDate DateTime { get; set; }
}