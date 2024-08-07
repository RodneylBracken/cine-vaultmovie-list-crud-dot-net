using System.ComponentModel.DataAnnotations;

namespace CineVault.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Title { get; set; }

        [Required]
        [RegularExpression("VHS|DVD|Streaming", ErrorMessage = "Allowed values: VHS, DVD, Streaming")]
        public string Format { get; set; }

        [Range(0, 500)]
        public int Length { get; set; }

        [Range(1800, 2100)]
        public int ReleaseYear { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
