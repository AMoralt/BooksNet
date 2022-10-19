using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class Price : ValueObject
{
    public int Value { get; }

    public Price(int price)
    {
        if(price < 0) throw new Exception("Price cannot be negative");
        
        Value = price;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}