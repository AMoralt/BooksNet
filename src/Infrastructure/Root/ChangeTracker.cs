using System.Collections.Concurrent;
using Domain.Root;
using Infrastructure.Contracts;

namespace Infrastructure.Root;

public class ChangeTracker : IChangeTracker
{
    public IEnumerable<Entity> TrackedEntities => _usedEntitiesBackingField.ToArray();

    // Можно заменить на любую другую имплементацию. Не только через ConcurrentBag
    private readonly ConcurrentBag<Entity> _usedEntitiesBackingField;

    public ChangeTracker()
    {
        _usedEntitiesBackingField = new ConcurrentBag<Entity>();
    }
        
    public void Track(Entity entity)
    {
        _usedEntitiesBackingField.Add(entity);
    }
}