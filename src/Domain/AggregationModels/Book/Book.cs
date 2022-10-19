using Domain.DomainEvents;
using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class Book : Entity
{
    public Book(
        int id,
        Title title, 
        ISBN isbn, 
        Quantity quantity, 
        Genre genre, 
        BookFormat format, 
        Price price, 
        PublicationDate publicationDate, 
        IReadOnlyCollection<Author> authors, 
        Publisher publisher)
    {
        Id = id;
        Title = title;
        ISBN = isbn;
        Quantity = quantity;
        Genre = genre;
        Format = format;
        Price = price;
        PublicationDate = publicationDate;
        Authors = authors;
        Publisher = publisher;
    }
    public Title Title { get; }
    public ISBN ISBN { get; }
    public Quantity Quantity { get; private set; }
    public Genre Genre { get; }
    public BookFormat Format { get; }
    public Price Price { get; private set; }
    public PublicationDate PublicationDate { get; }
    public IReadOnlyCollection<Author> Authors { get; }
    public Publisher Publisher { get; }
    
    public void IncreaseQuantity(int valueToIncrease)
    {
        Quantity = new Quantity(Quantity.Value + valueToIncrease);
    }
    
    public void GiveOutBook(int valueToGiveOut)
    {
        if(Quantity.Value - valueToGiveOut < 0)
            throw new InvalidOperationException("Quantity cannot be less than 0");
        
        Quantity = new Quantity(Quantity.Value - valueToGiveOut);

        if (Quantity.Value < Quantity.MinimalValue)
            this.AddDomainEvent(new MinNumberOfBooksDomainEvent(ISBN));
    }
}
