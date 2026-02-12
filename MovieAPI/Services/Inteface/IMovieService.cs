using MovieWatchlist.MovieAPI.Models.DTOs;
using MovieWatchlist.MovieAPI.Models.Entities;

namespace MovieWatchlist.MovieAPI.Services.Interface
{
    public interface IMovieService
    {
        List<Movie> GetAll();
        Movie GetById(int id);
        Movie CreateMovie(CreateMovie dto);
        void UpdateRating(int id, UpdateMovieRating dto);
        void UpdateMovie(int id, CreateMovie dto);
        void DeleteMovie(int id);
    }
}
