using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;


public class Genre : Enumeration
{
    public static Genre Fiction  = new(1, "Фантастика");
    public static Genre Drama  = new(2, "Драма");
    public static Genre Action  = new(3, "Экшн" );
    public static Genre Adventure  = new(4, "Приключение");
    public static Genre Romance  = new(5, "Роман");
    public static Genre Mystery  = new(6, "Мистика");
    public static Genre Horror  = new(7, "Хоррор");
    public static Genre Prose  = new(8, "Проза");
    public Genre(int id, string name) : base(id, name)
    {
        
    }
}