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

        // --- ОСНОВНИ И НОВИ ТЕСТОВЕ ---

        // Основни валидации (9 параметъра - съвпадат с метода долу)
        [TestCase(null, "Петров", 10, 25, "Outside Hitter", 0, 0, 0, "Липсва Име")]
        [TestCase("Иван", null, 10, 25, "Outside Hitter", 0, 0, 0, "Липсва Фамилия")]
        [TestCase("Иван", "Петров", 0, 25, "Outside Hitter", 0, 0, 0, "Номер е 0")]
        [TestCase("Иван", "Петров", 100, 25, "Outside Hitter", 0, 0, 0, "Номер е 100")]
        [TestCase("Иван", "Петров", 10, 9, "Outside Hitter", 0, 0, 0, "Възраст под 10")]
        [TestCase("Иван", "Петров", 10, 51, "Outside Hitter", 0, 0, 0, "Възраст над 60")]
        [TestCase("Иван", "Петров", 10, 25, null, 0, 0, 0, "Липсва позиция")]

        // Ръст (Range 150-250)
        [TestCase("Иван", "Иванов", 10, 25, "Setter", 149, 0, 0, "Ръст под 150")]
        [TestCase("Иван", "Иванов", 10, 25, "Setter", 251, 0, 0, "Ръст над 250")]

        // Размах (Range 150-350)
        [TestCase("Иван", "Иванов", 10, 25, "Setter", 200, 140, 0, "Размах под 150")]
        [TestCase("Иван", "Иванов", 10, 25, "Setter", 200, 360, 0, "Размах над 350")]

        // Скок (Range 20-130)
        [TestCase("Иван", "Иванов", 10, 25, "Setter", 200, 0, 15, "Скок под 20")]
        [TestCase("Иван", "Иванов", 10, 25, "Setter", 200, 0, 135, "Скок над 130")]

        // Възраст (Range 10-60)
        [TestCase("Иван", "Иванов", 10, 9, "Setter", 200, 0, 0, "Възраст под 10")]
        [TestCase("Иван", "Иванов", 10, 61, "Setter", 200, 0, 0, "Възраст над 60")]

        // Позиция (Required)
        [TestCase("Иван", "Иванов", 10, 25, null, 200, 0, 0, "Липсва позиция")]

        // Допълнителни комбинаторни тестове
        [TestCase("Иван", "Иванов", 10, 8, "Setter", 200, 200, 80, "Възраст под 10")]

        // --- ТОВА Е МЕТОДЪТ ТРЯБВА ДА Е ТОЧНО ТУК ---
        public void Player_WithInvalidData_ShouldFail(string? fname, string? lname, int shirt, int age, string? pos, int height, int wingspan, int jump, string errorMessage)
        {
            var player = new Player
            {
                FirstName = fname,
                LastName = lname,
                ShirtNumber = shirt,
                Age = age,
                Position = pos,
                Height = height,
                WingSpan = wingspan,
                VerticalJump = jump
            };

            var results = ModelValidator.Validate(player);
            Assert.IsNotEmpty(results, $"Трябва да има грешка за: {errorMessage}");
        }
    }
}