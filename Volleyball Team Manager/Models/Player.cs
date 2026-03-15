using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VolleyballManager.Data.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете име!")]
        [StringLength(50, ErrorMessage = "Името не може да е по-дълго от 50 символа.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Моля, въведете фамилия!")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Моля, въведете номер на фланелката!")]
        [Range(1, 99, ErrorMessage = "Номерът трябва да е между 1 и 99.")]
        public int ShirtNumber { get; set; }

        [Required(ErrorMessage = "Изберете позиция!")]
        [StringLength(20)]
        public string Position { get; set; }

        [Range(150, 250, ErrorMessage = "Ръстът трябва да е между 150 и 250 см.")]
        public int Height { get; set; }

        public int WingSpan { get; set; }
        public int VerticalJump { get; set; }
        public int Age { get; set; }
    }
}