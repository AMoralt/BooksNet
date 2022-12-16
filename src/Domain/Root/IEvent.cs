namespace Domain.Root;

public interface IEvent
{
    public string Name => GetType().Name;
    public string ISBN { get; }
    public int Quantity { get; }
    public int Price { get; }
}