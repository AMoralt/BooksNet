using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class BookDetails : ValueObject
{
    public string ISBN { get; }
    public DateTimeOffset PublicationDate { get; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public int MinimalQuantity { get; } = 5;
    
    private BookDetails(){}

    public BookDetails( int quantity, int price, DateTimeOffset publicationDate, string isbn)
    {
        if(quantity <= 0)
            throw new Exception("Quantity must be greater than zero");
        
        if(price < 0) 
            throw new Exception("Price cannot be negative");
        
        if(publicationDate > DateTimeOffset.Now)
            throw new ArgumentException("Publication date cannot be in the future");
        
        if (string.IsNullOrWhiteSpace(isbn))
            throw new Exception("ISBN should not be empty");
        if (isbn.Length != 10 && isbn.Length != 13) 
            throw new Exception("ISBN should contain 10 or 13 digits");
        if (isbn.Any(c => !char.IsDigit(c)))
            throw new Exception("ISBN should contain only digits");

        //Format = BookFormat.GetAll<BookFormat>().FirstOrDefault(item => item.Name == format)
          //       ?? BookFormat.Comic;//?? throw new ArgumentException("Invalid book format");

        ISBN = isbn;
        PublicationDate = publicationDate;
        Price = price;
        Quantity = quantity;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ISBN;
        yield return PublicationDate;
        yield return Price;
        yield return Quantity;
    }
}