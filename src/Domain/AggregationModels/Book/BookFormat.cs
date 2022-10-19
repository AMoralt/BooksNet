using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class BookFormat : Enumeration
{
    public static BookFormat HardCover  = new(1, nameof(HardCover));
    public static BookFormat PaperBack  = new(2, nameof(PaperBack ));
    public static BookFormat EBook  = new(3, nameof(EBook ));
    public static BookFormat AudioBook  = new(4, nameof(AudioBook ));
    public static BookFormat Magazine  = new(5, nameof(Magazine ));
    public static BookFormat Newspaper  = new(6, nameof(Newspaper ));
    public static BookFormat Comic  = new(7, nameof(Comic ));
    public BookFormat(int id, string name) : base(id, name)
    {
        
    }
}