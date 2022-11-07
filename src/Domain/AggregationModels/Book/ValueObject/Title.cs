using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class Title : ValueObject
{
    public string Value { get; }
    //for dapper contructor
    public Title(string title)
    {
        Value = title;
    }
    public static Title Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentNullException(nameof(title));
        }
        return new Title(title);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}