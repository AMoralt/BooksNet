using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class Title : ValueObject
{
    public string Value { get; }
    public Title(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title));
        
        Value = title;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}