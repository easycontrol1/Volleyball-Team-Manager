using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using VolleyballManager.Data;
using VolleyballManager.Data.Models;
using VolleyballManager.Services;

namespace VolleyballManager.Tests
{
    [TestFixture]
    public class StatisticsServiceTests
    {
        private ApplicationDbContext context;
        private StatisticsService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            service = new StatisticsService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        // ТЕСТ 1: Добавяне на нова статистика
        [Test]
        public void AddStatistic_ShouldCreateRecord()
        {
            // Arrange
            var match = new Match { Date = DateTime.Now, Opponent = "Левски", Result = "3:0", IsHomeGame = true };
            var player = new Player { FirstName = "Иван", LastName = "Иванов", ShirtNumber = 10, Age = 25, Position = "Setter", Height = 190 };

            context.Matches.Add(match);
            context.Players.Add(player);
            context.SaveChanges();

            var stat = new PlayerStatistic
            {
                PlayerId = player.Id,
                MatchId = match.Id,
                AttackPoints = 5,
                ServicePoints = 2,
                BlockPoints = 1
            };

            // Act
            service.AddStatistic(stat);

            // Assert
            Assert.AreEqual(1, context.PlayerStatistics.Count());
            var dbStat = context.PlayerStatistics.First();
            Assert.AreEqual(5, dbStat.AttackPoints);
        }

        // ТЕСТ 2: Ъпдейт на съществуваща статистика (Ако вече вкарал точки за този мач)
        [Test]
        public void AddStatistic_ShouldUpdateExistingRecord()
        {
            // Arrange
            var match = new Match { Date = DateTime.Now, Opponent = "Левски", Result = "3:0", IsHomeGame = true };
            var player = new Player { FirstName = "Иван", LastName = "Иванов", ShirtNumber = 10, Age = 25, Position = "Setter", Height = 190 };

            context.Matches.Add(match);
            context.Players.Add(player);
            context.SaveChanges();

            
            service.AddStatistic(new PlayerStatistic { PlayerId = player.Id, MatchId = match.Id, AttackPoints = 5 });

            
            service.AddStatistic(new PlayerStatistic { PlayerId = player.Id, MatchId = match.Id, AttackPoints = 3 });

            // Assert
            Assert.AreEqual(1, context.PlayerStatistics.Count());
            var dbStat = context.PlayerStatistics.First();
            Assert.AreEqual(3, dbStat.AttackPoints);
        }

        // ТЕСТ 3: Зареждане на статистика с имена (Include)
        [Test]
        public void GetStatisticsByMatch_ShouldIncludePlayerNames()
        {
            // Arrange
            var match = new Match { Date = DateTime.Now, Opponent = "ЦСКА", Result = "0:3", IsHomeGame = false };
            var player = new Player { FirstName = "Георги", LastName = "Георгиев", ShirtNumber = 12, Age = 22, Position = "Libero", Height = 180 };

            context.Matches.Add(match);
            context.Players.Add(player);
            context.SaveChanges();

            service.AddStatistic(new PlayerStatistic { PlayerId = player.Id, MatchId = match.Id, AttackPoints = 10 });

            // Act
            var stats = service.GetStatisticsByMatch(match.Id);

            // Assert
            Assert.AreEqual(1, stats.Count());
            Assert.IsNotNull(stats.First().Player); // Проверяваме дали Player обектът е зареден
            Assert.AreEqual("Георги", stats.First().Player.FirstName);
        }
    }
}