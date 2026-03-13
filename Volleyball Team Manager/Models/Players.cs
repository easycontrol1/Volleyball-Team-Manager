using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolleyballManager.Data.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Range(1, 99)]
        public int ShirtNumber { get; set; }

        [StringLength(20)]
        public string Position { get; set; } // напр. "Libero", "Setter"

        public int Height { get; set; } // в см

        public int WingSpan { get; set; } // в см 

        public int VerticalJump { get; set; } // в см

        public int Age { get; set; }

    }
}