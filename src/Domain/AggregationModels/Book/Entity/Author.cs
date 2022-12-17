using Domain.Root;

namespace Domain.AggregationModels.Book;

public class Author : Entity
{
    private Author(){}
    public string FirstName { get; }
    public string LastName { get; }
    public Author(int? id, string lastName, string firstName)
    {
        Id = id;
        LastName = lastName;
        FirstName = firstName;
    }
    
    public static Author Create(int? id, string lastName, string firstName)
    {
        if(string.IsNullOrEmpty(lastName))
            throw new Exception("Author lastName is required");
        if(!string.Concat(lastName.Where(c=>!char.IsWhiteSpace(c))).All(char.IsLetter) )
            throw new Exception("Author lastName must contain only letters");
        if(lastName.Length > 50) 
            throw new Exception("Author lastName must be less than 50 characters");
        
        if(string.IsNullOrEmpty(firstName))
            throw new Exception("Author firstName is required");
        if(!string.Concat(firstName.Where(c=>!char.IsWhiteSpace(c))).All(char.IsLetter) )
            throw new Exception("Author firstName must contain only letters");
        if(firstName.Length > 50) 
            throw new Exception("Author firstName must be less than 50 characters");
        
        return new Author(id, lastName, firstName);
    }
}