using NUnit.Framework;
using System.Linq;
using VolleyballManager.Data.Models;
using VolleyballManager.Tests.Helpers;

namespace VolleyballManager.Tests
{
    [TestFixture]
    public class MatchValidationTests
    {
        [Test]
        public void Match_WithValidData_ShouldPass()
        {
            var match = new Match
            {
                Date = DateTime.Parse("2024-05-20"),
                Opponent = "Левски",
                Result = "3:0",
                IsHomeGame = true
            };
            var results = ModelValidator.Validate(match);
            Assert.IsEmpty(results);
        }

        // 1. Празен текст за Липсваща Дата
        [TestCase("", "Левски", "3:0", "Липсва Дата")]

        // 2. Валидна дата, но NULL за Противник
        [TestCase("2024-01-01", null, "3:0", "Липсва Противник")]
        [TestCase("2024-01-01", "Лубе", null, "Липсва Резултат")]


        public void Match_WithInvalidData_ShouldFail(string dateString, string? opponent, string? result, string errorMessage)
        {
            DateTime? date = null; // По подразбиране е null (това ще хване грешката)

            if (!string.IsNullOrEmpty(dateString))
            {
                DateTime.TryParse(dateString, out DateTime parsedDate);
                date = parsedDate;
            }

            var match = new Match
            {
                Date = date,
                Opponent = opponent,
                Result = result,
                IsHomeGame = true
            };

            var results = ModelValidator.Validate(match);
            Assert.IsNotEmpty(results, $"Трябва да има грешка за: {errorMessage}");
        }
    }
}