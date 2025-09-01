namespace CleanArch.UnitTests;

public class BaseAuditableEntityTests
{
    public class TestAuditableEntity : BaseAuditableEntity;

    [Fact]
    public void Ctor_CreatesAuditableEntity()
    {
        // Arrange
        var createdOn = DateTimeOffset.UtcNow;
        var createdBy = "User1";
        var lastModifiedOn = DateTimeOffset.UtcNow;
        var lastModifiedBy = "User2";

        // Act
        var auditableEntity = new TestAuditableEntity
        {
            CreatedOn = createdOn,
            CreatedBy = createdBy,
            LastModifiedOn = lastModifiedOn,
            LastModifiedBy = lastModifiedBy
        };

        // Assert
        Assert.Equal(createdOn, auditableEntity.CreatedOn);
        Assert.Equal(createdBy, auditableEntity.CreatedBy);
        Assert.Equal(lastModifiedOn, auditableEntity.LastModifiedOn);
        Assert.Equal(lastModifiedBy, auditableEntity.LastModifiedBy);
    }
}
