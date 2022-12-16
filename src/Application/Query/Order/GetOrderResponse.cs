
namespace Application.Query;

public record GetOrderResponse(
    int id,
    DateTime PurchaseDate,
    List<OrderItemResponse> order_items);
    
    public record OrderItemResponse(
        int Quantity,
        int Price,
        GetBookResponse books);