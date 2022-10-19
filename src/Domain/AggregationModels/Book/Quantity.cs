using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class Quantity : ValueObject
{
    public int Value { get; }
    public int MinimalValue { get; } = 5;
    public Quantity(int quantity)
    {
        if(quantity <= 0)
            throw new Exception("Quantity must be greater than zero");
        
        Value = quantity;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}