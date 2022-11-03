using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class BookFormat : Enumeration
{
    public static BookFormat HardCover  = new(1, "Твердая бумажная");
    public static BookFormat PaperBack  = new(2, "Мягкая бумажная");
    public static BookFormat EBook  = new(3, "Электронная книга");
    public static BookFormat AudioBook  = new(4, "Аудио-книга");
    public static BookFormat Magazine  = new(5, "Журнал");
    public static BookFormat Newspaper  = new(6, "Газета");
    public static BookFormat Comic  = new(7, "Комикс");
    public BookFormat(int id, string name) : base(id, name)
    {
        
    }
}