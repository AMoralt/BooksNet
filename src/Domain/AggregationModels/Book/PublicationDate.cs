using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class PublicationDate : ValueObject
{
    public DateTimeOffset Value { get; }

    public PublicationDate(DateTimeOffset publicationDate)
    {
        if(publicationDate > DateTimeOffset.Now)
            throw new ArgumentException("Publication date cannot be in the future");
        
        Value = publicationDate;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}