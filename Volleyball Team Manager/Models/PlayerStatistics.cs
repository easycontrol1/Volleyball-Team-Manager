using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolleyballManager.Data.Models
{
    public class PlayerStatistic
    {
        [Key]
        public int Id { get; set; }

        // Връзка с Играча (Foreign Key)
        [Required]
        public int PlayerId { get; set; }

        // Навигационно свойство (за лесно достъпване на данните от играча)
        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }

        // Връзка с Мача (Foreign Key)
        [Required]
        public int MatchId { get; set; }

        // Навигационно свойство (за лесно достъпване на данните от мача)
        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; }

        // --- Данни за статистиката ---

        // Точки при сервис
        [Range(0, 50)]
        public int ServicePoints { get; set; }

        // Точки при атака
        [Range(0, 100)]
        public int AttackPoints { get; set; }

        // Точки при блок
        [Range(0, 100)]
        public int BlockPoints { get; set; }

        // Посрещане (брой позитивни посрещания)
        [Range(0, 100)]
        public int PositiveReceptions { get; set; }

        // Общи грешки
        [Range(0, 50)]
        public int Errors { get; set; }
    }
}