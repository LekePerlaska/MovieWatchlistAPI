namespace MovieWatchlist.MovieAPI.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int DurationMinutes { get; set; }
        public int? Rating { get; set; }
    }
}
