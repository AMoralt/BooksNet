namespace EmptyProjectASPNETCORE;

public interface IRepository<T>
{
    IUnitOfWork UnitOfWork { get; }
}