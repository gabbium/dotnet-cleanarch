namespace CleanArch.UnitTests;

public class SpecificationTests
{
    public record TestEntity(string Name, bool Active);

    public class AllTestEntitiesSpec : Specification<TestEntity> { }

    public class ActiveTestEntitiesSpec : Specification<TestEntity>
    {
        public ActiveTestEntitiesSpec()
        {
            Criteria = e => e.Active;
        }
    }

    private static readonly List<TestEntity> s_data =
    [
        new("ABC", true),
        new("DEF", false),
        new("XYZ", true),
    ];

    [Fact]
    public void DefaultCriteria_MatchesAll()
    {
        // Arrange
        var spec = new AllTestEntitiesSpec();

        // Act
        var result = s_data.Where(spec.Criteria.Compile()).ToList();

        // Assert
        Assert.Equal(s_data.Count, result.Count);
    }

    [Fact]
    public void DerivedSpec_FiltersCorrectly()
    {
        // Arrange
        var spec = new ActiveTestEntitiesSpec();

        // Act
        var result = s_data.Where(spec.Criteria.Compile()).ToList();

        // Assert
        Assert.All(result, p => Assert.True(p.Active));
        Assert.Equal(2, result.Count);
    }
}
