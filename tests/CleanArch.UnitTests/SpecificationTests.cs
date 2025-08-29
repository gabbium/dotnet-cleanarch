namespace CleanArch.UnitTests;

public class SpecificationTests
{
    public record TestEntity(string Name, bool Active);

    public sealed class AllTestEntitiesSpec : Specification<TestEntity> { }

    public sealed class ActiveTestEntitiesSpec : Specification<TestEntity>
    {
        public ActiveTestEntitiesSpec()
        {
            Criteria = e => e.Active;
        }
    }

    private static readonly List<TestEntity> Data =
    [
        new("ABC", true),
        new("DEF", false),
        new("XYZ", true),
    ];

    [Fact]
    public void DefaultCriteria_MatchesAll()
    {
        var spec = new AllTestEntitiesSpec();

        var result = Data.Where(spec.Criteria.Compile()).ToList();

        Assert.Equal(Data.Count, result.Count);
    }

    [Fact]
    public void DerivedSpec_FiltersCorrectly()
    {
        var spec = new ActiveTestEntitiesSpec();

        var result = Data.Where(spec.Criteria.Compile()).ToList();

        Assert.All(result, p => Assert.True(p.Active));
        Assert.Equal(2, result.Count);
    }
}
