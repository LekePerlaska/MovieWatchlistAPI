using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.MovieAPI.Models.DTOs
{
    public class CreateMovie
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Director { get; set; }

        [Range(1, 500)]
        public int DurationMinutes { get; set; }
    }
}
