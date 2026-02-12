using MovieWatchlist.MovieAPI.Data;
using MovieWatchlist.MovieAPI.Models.DTOs;
using MovieWatchlist.MovieAPI.Models.Entities;
using MovieWatchlist.MovieAPI.Services.Interface;

namespace MovieWatchlist.MovieAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDB _context;

        public MovieService(MovieDB context)
        {
            _context = context;
        }

        public List<Movie> GetAll()
        {
            return _context.GetAll();
        }

        public Movie GetById(int id)
        {
            var movie = _context.GetAll().FirstOrDefault(m => m.Id == id);

            return movie;
        }

        public Movie CreateMovie(CreateMovie dto)
        {
            var movies = _context.GetAll();

            var movie = new Movie
            {
                Id = movies.Any() ? movies.Max(m => m.Id) + 1 : 1,
                Title = dto.Title,
                Director = dto.Director,
                DurationMinutes = dto.DurationMinutes,
                Rating = null, // No rating at creation
            };

            _context.Add(movie);
            return movie;
        }

        public void UpdateRating(int id, UpdateMovieRating dto)
        {
            if (dto.Rating < 1 || dto.Rating > 10)
                throw new ArgumentException("Rating must be between 1 and 10.");

            var movies = _context.GetAll();
            var movie = movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
                throw new Exception("Movie not found");

            movie.Rating = dto.Rating;

            _context.SaveAll(movies);
        }

        public void UpdateMovie(int id, CreateMovie dto)
        {
            var movie = new Movie
            {
                Id = id,
                Title = dto.Title,
                Director = dto.Director,
                DurationMinutes = dto.DurationMinutes,
                Rating = null,
            };

            _context.Update(movie);
        }

        public void DeleteMovie(int id)
        {
            _context.Delete(id);
        }
    }
}
