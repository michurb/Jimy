using Xunit;
using Microsoft.EntityFrameworkCore;
using Jimy.Api.Entities;
using System.Linq;

public class TrainingSessionServiceTests
{
    private JimyDbContext _context;
    private TrainingSessionSerivce _service;

    public TrainingSessionServiceTests()
    {
        var options = new DbContextOptionsBuilder<JimyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new JimyDbContext(options);
        _service = new TrainingSessionSerivce(_context);
    }

    [Fact]
    public void GetAll_ReturnsAllTrainingSessions()
    {
        // Arrange
        var session = new TrainingSession { Name = "TestSession" };
        _context.TrainingSessions.Add(session);
        _context.SaveChanges();

        // Act
        var result = _service.GetAll();

        // Assert
        Assert.Single(result);
        Assert.Equal("TestSession", result.First().Name);
    }

    [Fact]
    public void GetById_ReturnsCorrectTrainingSession()
    {
        // Arrange
        var session = new TrainingSession { Name = "TestSession" };
        _context.TrainingSessions.Add(session);
        _context.SaveChanges();

        // Act
        var result = _service.GetById(session.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("TestSession", result.Name);
    }

    // ... Continue writing tests for other methods in a similar fashion

    // After all tests, clean up the in-memory database
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
    }
}
