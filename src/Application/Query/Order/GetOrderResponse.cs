
namespace Application.Query;

public record GetOrderResponse(
    int id,
    DateTime PurchaseDate,
    List<OrderItemResponse> order_items);
    
    public record OrderItemResponse(
        int Quantity,
        int Price,
        OrderBookResponse books);
        
public record OrderBookResponse(
    string title,
    string ISBN,
    string genre,
    string publisherName,
    DateTime publicationDate,
    List<AuthorResponse> authors,
    string bookFormat);