using NUnit.Framework;
using System.Linq;
using VolleyballManager.Data.Models;
using VolleyballManager.Tests.Helpers;

namespace VolleyballManager.Tests
{
    [TestFixture]
    public class PlayerValidationTests
    {
        [Test]
        public void Player_WithValidData_ShouldPass()
        {
            var player = new Player
            {
                FirstName = "Иван",
                LastName = "Петров",
                ShirtNumber = 10,
                Age = 25,
                Position = "Outside Hitter",
                Height = 190,
                WingSpan = 200,
                VerticalJump = 80
            };

            var results = ModelValidator.Validate(player);
            Assert.IsEmpty(results, "Валидният играч не трябва да има грешки.");
        }

        // Виж как съ добавил "?" след string? Това позволява да се подава null.
        [TestCase(null, "Петров", 10, 25, "Outside Hitter", "Липсва Име")]
        [TestCase("Иван", null, 10, 25, "Outside Hitter", "Липсва Фамилия")]
        [TestCase("Иван", "Петров", 0, 25, "Outside Hitter", "Номер е 0")]
        [TestCase("Иван", "Петров", 100, 25, "Outside Hitter", "Номер е 100")]
        [TestCase("Иван", "Петров", 10, 9, "Outside Hitter", "Възраст под 10")]
        [TestCase("Иван", "Петров", 10, 51, "Outside Hitter", "Възраст над 50")]
        [TestCase("Иван", "Петров", 10, 25, null, "Липсва позиция")]
        public void Player_WithInvalidData_ShouldFail(string? fname, string? lname, int shirt, int age, string? pos, string errorMessage)
        {
            var player = new Player
            {
                FirstName = fname,
                LastName = lname,
                ShirtNumber = shirt,
                Age = age,
                Position = pos,
                Height = 140 // Ръст под 150
            };

            var results = ModelValidator.Validate(player);
            Assert.IsNotEmpty(results, $"Трябва да има грешка за: {errorMessage}");
        }
    }
}