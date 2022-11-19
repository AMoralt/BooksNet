using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class BookFormat : Entity
{
    public string Name { get; }
    public BookFormat(int? id, string name)
    {
        Id = id;
        Name = name;
    }
    public static BookFormat Create(int? id, string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new Exception("BookFormat name is required");
        if(!string.Concat(name.Where(c=>!char.IsWhiteSpace(c))).All(char.IsLetter) )
            throw new Exception("BookFormat name must contain only letters");
        if(name.Length > 200) 
            throw new Exception("BookFormat name must be less than 200 characters");
        
        return new BookFormat(id, name);
    }
}