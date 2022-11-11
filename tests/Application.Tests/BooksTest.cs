using Domain.AggregationModels.Book;
using EmptyProjectASPNETCORE;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing.Printing;
using TemplateASP.NET.CORE.Query;

namespace Application.Tests;

public class BooksTest
{
    [Fact]
    public void Test_GetAllBooksQuery()
    {
        //test GetAllBooksQuery
        var mock = new Mock<IMediator>();
        var getAllBooksQuery = new GetAllBooksQuery();
        var result = mock.Object.Send(getAllBooksQuery);
        Assert.NotNull(result);
    }

    [Fact]
    public void Test_GetAll()
    {
        var mediator = new Mock<IMediator>();

        BookController controller = new BookController(mediator);
        var books = controller.GetAll(new CancellationToken());

        Assert.Equal(GetTestBooks().Count(), books.Count());
    }
    private IEnumerable<Book> GetTestBooks()
    {
        var books = new List<Book>
        {
        new Book()
        };
        return books;
    }
    
}

[Fact]
    public void Test_GetByISBNBookQuery()
    {
        //test GetByISBNBookQuery
        var mock = new Mock<IMediator>();
        var getByISBNBookQuery = new GetByISBNBookQuery("9785041549435");
        var result = mock.Object.Send(getByISBNBookQuery);
        Assert.NotNull(result);
        Assert.Equal("9785041549435", result.Result.ISBN);
    }

    
}

// [FACT - метод без параметров.]