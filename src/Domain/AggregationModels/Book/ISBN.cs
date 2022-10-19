using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class ISBN : ValueObject
{
    public string Value { get; }

    public ISBN(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            throw new Exception("ISBN should not be empty");
        if (isbn.Length != 10 && isbn.Length != 13)
            throw new Exception("ISBN should contain 10 or 13 digits");
        if (isbn.Any(c => !char.IsDigit(c)))
            throw new Exception("ISBN should contain only digits");
        
        Value = isbn;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}