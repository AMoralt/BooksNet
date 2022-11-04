using Domain.DomainEvents;
using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class Book : Entity
{
    private Book(){}
    public Book(
        int id,
        BookDetails details,
        Title title, 
        Genre genre,
        List<Author> authors, 
        Publisher publisher,
        BookFormat format)
    {
        Id = id;
        Title = title;
        Details = details;
        Genre = genre;
        Authors = authors;
        Publisher = publisher;
        Format = format;
    }
    public Title Title { get; set;}
    public Genre Genre { get; set; }
    public BookFormat Format { get; set; }
    public List<Author> Authors { get; set; } 
    public Publisher Publisher { get; set;}
    public BookDetails Details { get; set;}
    
    public void IncreaseQuantity(int valueToIncrease)
    {
        Details.Quantity += valueToIncrease;
    }
    
    public void DecreaseQuantity(int valueToGiveOut)
    {
        if(Details.Quantity - valueToGiveOut < 0)
            throw new InvalidOperationException("Quantity cannot be less than 0");
        
        Details.Quantity -= valueToGiveOut;

        if (Details.Quantity < Details.MinimalQuantity)
            this.AddDomainEvent(new MinNumberOfBooksDomainEvent(Details.ISBN));
    }
}
