using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using VolleyballManager.Data;
using VolleyballManager.Data.Models;
using VolleyballManager.Services;

namespace VolleyballManager.Tests
{
    [TestFixture]
    public class PlayerServiceTests
    {
        private ApplicationDbContext context;
        private PlayerService service;

        [SetUp]
        public void Setup()
        {
            // Създаваме база в паметта
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            service = new PlayerService(context);
        }

        
        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        // ТЕСТ 1: Добавяне
        [Test]
        public void AddPlayer_ShouldAddToContext()
        {
            var player = new Player { FirstName = "Георги", LastName = "Димитров", ShirtNumber = 8, Age = 22, Position = "Libero", Height = 185 };
            service.AddPlayer(player);
            Assert.AreEqual(1, context.Players.Count());
        }

        // ТЕСТ 2: Извличане на всички
        [Test]
        public void GetAllPlayers_WithTwoPlayers_ShouldReturnTwo()
        {
            service.AddPlayer(new Player { FirstName = "А", LastName = "Б", ShirtNumber = 1, Age = 20, Position = "Setter", Height = 180 });
            service.AddPlayer(new Player { FirstName = "В", LastName = "Г", ShirtNumber = 2, Age = 21, Position = "Middle Blocker", Height = 190 });

            var players = service.GetAllPlayers();
            Assert.AreEqual(2, players.Count());
        }

        // ТЕСТ 3: Редактиране
        [Test]
        public void UpdatePlayer_ShouldModifyExistingPlayer()
        {
            var player = new Player { FirstName = "Старо", LastName = "Име", ShirtNumber = 10, Age = 25, Position = "Setter", Height = 190 };
            service.AddPlayer(player);

            player.Age = 26;
            player.FirstName = "Ново";
            service.UpdatePlayer(player);

            var updatedPlayer = context.Players.FirstOrDefault(p => p.Id == player.Id);
            Assert.AreEqual(26, updatedPlayer.Age);
            Assert.AreEqual("Ново", updatedPlayer.FirstName);
        }

        // ТЕСТ 4: Изтриване
        [Test]
        public void DeletePlayer_ShouldRemoveFromContext()
        {
            var player = new Player { FirstName = "Три", LastName = "Йон", ShirtNumber = 99, Age = 20, Position = "Libero", Height = 180 };
            service.AddPlayer(player);
            var id = player.Id;

            service.DeletePlayer(id);

            Assert.AreEqual(0, context.Players.Count());
        }

        // ТЕСТ 5: Грешен ID
        [Test]
        public void DeletePlayer_WithInvalidId_ShouldDoNothing()
        {
            Assert.DoesNotThrow(() => service.DeletePlayer(9999));
            Assert.AreEqual(0, context.Players.Count());
        }
    }
}