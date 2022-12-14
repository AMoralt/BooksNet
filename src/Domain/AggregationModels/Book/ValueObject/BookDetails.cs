using Domain.Root;

namespace Domain.AggregationModels.Book;

public class BookDetails : ValueObject
{
    public string ISBN { get; }
    public DateTime PublicationDate { get; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public int MinimalQuantity { get; } = 5;
    
    private BookDetails(){}
    //for dapper contructor
    public BookDetails(int quantity, int price, DateTime publicationDate, string isbn)
    {
        ISBN = isbn;
        PublicationDate = publicationDate;
        Price = price;
        Quantity = quantity;
    }
    public static BookDetails Create(int quantity, int price, DateTime publicationDate, string isbn)
    {
        if(quantity <= 0)
            throw new Exception("Quantity must be greater than zero");
        
        if(price <= 0) 
            throw new Exception("Price cannot be negative");
        
        if(publicationDate > DateTime.Now)
            throw new ArgumentException("Publication date cannot be in the future");
        
        if (string.IsNullOrWhiteSpace(isbn))
            throw new Exception("ISBN should not be empty");
        if (isbn.Length != 10 && isbn.Length != 13) 
            throw new Exception("ISBN should contain 10 or 13 digits");
        if (isbn.Any(c => !char.IsDigit(c)))
            throw new Exception("ISBN should contain only digits");
        
        return new BookDetails(quantity, price, publicationDate, isbn);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ISBN;
        yield return PublicationDate;
        yield return Price;
        yield return Quantity;
    }
}