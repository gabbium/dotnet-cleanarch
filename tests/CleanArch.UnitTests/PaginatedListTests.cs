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
        paged.Items.ShouldBe(items);
        paged.TotalItems.ShouldBe(totalItems);
        paged.PageNumber.ShouldBe(pageNumber);
        paged.PageSize.ShouldBe(pageSize);
        paged.TotalPages.ShouldBe(5);
        paged.HasPreviousPage.ShouldBeTrue();
        paged.HasNextPage.ShouldBeTrue();
    }
}
