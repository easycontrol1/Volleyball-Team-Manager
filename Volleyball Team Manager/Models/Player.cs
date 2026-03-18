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
        public string Position { set; get; }

        [Range(150, 250, ErrorMessage = "Ръстът трябва да е между 150 и 250 см.")]
        public int Height { get; set; }

        [Range(150, 350, ErrorMessage = "Размахът трябва да е между 150 и 350 см.")]
        public int WingSpan { get; set; }

        [Range(20, 130, ErrorMessage = "Скокът трябва да е между 20 и 130 см.")]
        public int VerticalJump { get; set; }

        [Range(10, 60, ErrorMessage = "Възрастта трябва да е между 10 и 60 години.")]
        public int Age { get; set; }
    }
}