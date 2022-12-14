using Domain.Root;

namespace Infrastructure.Contracts;

public interface IChangeTracker
{
    /// <summary>
    /// Коллекция всех сущностей, которые так или иначе были использованы в репозитории.
    /// </summary>
    IEnumerable<Entity> TrackedEntities { get; }

    /// <summary>
    /// "Записать" сущность как подлежащую "использованию" в рамках выполнения запроса.
    /// </summary>
    /// <param name="entity"></param>
    public void Track(Entity entity);
}