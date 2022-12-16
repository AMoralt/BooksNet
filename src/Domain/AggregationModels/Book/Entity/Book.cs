using Domain.DomainEvents;
using Domain.Root;

namespace Domain.AggregationModels.Book;

public class Book : Entity
{
    private Book(){}
    public Book(
        int? id,
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
        this.AddDomainEvent(new IncreaseQuantityDomainEvent(Details.ISBN, valueToIncrease, Details.Price));
    }
    
    public void DecreaseQuantity(int valueToGiveOut)
    {
        if(Details.Quantity - valueToGiveOut < 0)
            throw new InvalidOperationException("Quantity cannot be less than 0");
        
        Details.Quantity -= valueToGiveOut;
        
        this.AddDomainEvent(new DecreaseQuantityDomainEvent(Details.ISBN, valueToGiveOut, Details.Price));

        if (Details.Quantity < Details.MinimalQuantity)
            this.AddDomainEvent(new MinNumberOfBooksDomainEvent(Details.ISBN, 10, Details.Price));//TODO: 10 is a magic number
    }
}
