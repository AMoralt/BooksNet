using EmptyProjectASPNETCORE;
using MediatR;
using Moq;

namespace Application.Tests;

public class BooksTest
{
    [Fact]
    public void Test_GetAll()
    {
        var cancellationToken = new CancellationToken();
        //test controller GetAll with mock
        var mockMediator = new Mock<IMediator>();
        var controller = new BookController(mockMediator.Object);
        var result = controller.GetAll(cancellationToken);
        Assert.NotNull(result);
    }

    [Fact]
    public void Test_GetByISBN()
    {
        var cancellationToken = new CancellationToken();
        var mockMediator = new Mock<IMediator>();
        var controller = new BookController(mockMediator.Object);
        var result = controller.GetByISBN("9785171341671", cancellationToken);
        Assert.NotNull(result);
        //Assert.Equal("9785171341671", result);
    }


    [Fact]
    public void Test_DeleteByISBN()
    {
        var cancellationToken = new CancellationToken();
        var mockMediator = new Mock<IMediator>();
        var controller = new BookController(mockMediator.Object);
        var result = controller.DeleteByISBN("9785171341671", cancellationToken);
        Assert.NotNull(result);
    }

    [Fact]
    public void Test_Create()
    {
        var cancellationToken = new CancellationToken();
        var mockMediator = new Mock<IMediator>();
        var controller = new BookController(mockMediator.Object);
        var result = controller.DeleteByISBN("9785171341671", cancellationToken);
        Assert.NotNull(result);

    }

    [Fact]
    public void Test_Update()
    {
        var cancellationToken = new CancellationToken();
        var mockMediator = new Mock<IMediator>();
        var controller = new BookController(mockMediator.Object);
        var result = controller.DeleteByISBN("9785171341671", cancellationToken);
        Assert.NotNull(result);
    }
}

// [FACT - ����� ��� ����������.]