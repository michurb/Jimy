using Jimy.Api.Data;
using Jimy.Api.Entities;
using Jimy.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Jimy.Tests.Unit
{
   public class TrainingSessionServiceTests : IDisposable
    {
        private readonly JimyDbContext _context;
        private TrainingSessionService _trainingSessionService;
        private ExercisesService _exercisesService;

        public TrainingSessionServiceTests()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<JimyDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            
            _context = new JimyDbContext(options);
            _trainingSessionService = new TrainingSessionService(_context);
            _exercisesService = new ExercisesService(_context);
        }

        [Fact]
        public void GetAll_ReturnsAllTrainingSessions()
        {
                // Arrange
                var session = new TrainingSession { Name = "TestSession" };
                _context.TrainingSessions.Add(session);
                _context.SaveChanges();

                // Act
                var result = _trainingSessionService.GetAll();

                // Assert
                Assert.Single(result);
                Assert.Equal("TestSession", result.First().Name);
        }

        [Fact]
        public void GetById_ReturnsCorrectTrainingSession()
        {
                // Arrange
                var session = new TrainingSession { Name = "TestSession1" };
                _context.TrainingSessions.Add(session);
                _context.SaveChanges();

                // Act
                var result = _trainingSessionService.GetById(session.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("TestSession1", result.Name);
        }

        [Fact]
        public void Add_Exercise_Details_To_TrainingSession()
        {
            
                // Arrange
                var exercise = new Exercise
                {
                    Name = "TestExerciseDetails"
                };
                _exercisesService.Add(exercise);

                var trainingSession = new TrainingSession
                {
                    Name = "Session"
                };
                _trainingSessionService.AddSession(trainingSession);

                int reps = 10;
                int set = 10;

                // Act
                _trainingSessionService.AddExerciseToSession(trainingSession.Id, exercise.Id, reps, set);

                // Assert
                var addedTrainingSession = _trainingSessionService.GetById(trainingSession.Id);
                Assert.NotNull(addedTrainingSession);
                Assert.Single(addedTrainingSession.ExercisesDetails);
                Assert.Equal("TestExerciseDetails", addedTrainingSession.ExercisesDetails.First().Name);
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
