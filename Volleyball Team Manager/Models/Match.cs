using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolleyballManager.Data.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        [StringLength(100)]
        public string Opponent { get; set; } // Име на противниковия отбор

        [Required]
        [StringLength(50)]
        public string Result { get; set; } // напр. "3:1", "0:3"

        public bool IsHomeGame { get; set; } // Дали съм домакини
    }
}