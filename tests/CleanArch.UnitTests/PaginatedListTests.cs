namespace CleanArch.UnitTests;

public class PaginatedListTests
{
    [Fact]
    public void Ctor_CreatesPaginatedList()
    {
        // Arrange
        var items = new[] { "item1", "item2" };
        var totalItems = 10;
        var pageNumber = 2;
        var pageSize = 2;

        // Act
        var paged = new PaginatedList<string>(items, totalItems, pageNumber, pageSize);

        // Assert
        Assert.Equal(items, paged.Items);
        Assert.Equal(totalItems, paged.TotalItems);
        Assert.Equal(pageNumber, paged.PageNumber);
        Assert.Equal(pageSize, paged.PageSize);
        Assert.Equal(5, paged.TotalPages);
        Assert.True(paged.HasPreviousPage);
        Assert.True(paged.HasNextPage);
    }
}
