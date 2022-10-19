using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class Genre : Enumeration
{
    public static Genre Fiction  = new(1, nameof(Fiction));
    public static Genre Drama  = new(2, nameof(Drama ));
    public static Genre Action  = new(3, nameof(Action ));
    public static Genre Adventure  = new(4, nameof(Adventure ));
    public static Genre Romance  = new(5, nameof(Romance ));
    public static Genre Mystery  = new(6, nameof(Mystery ));
    public static Genre Horror  = new(7, nameof(Horror ));
    public Genre(int id, string name) : base(id, name)
    {
        
    }
}