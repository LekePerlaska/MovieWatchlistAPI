using System.Globalization;
using CsvHelper;
using MovieWatchlist.MovieAPI.Models.Entities;

namespace MovieWatchlist.MovieAPI.Data
{
    public class MovieDB
    {
        private readonly string _filePath;

        public MovieDB(string filePath)
        {
            _filePath = filePath;

            // Create file with header if it doesn't exist
            if (!File.Exists(_filePath))
            {
                using var writer = new StreamWriter(_filePath);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteHeader<Movie>();
                csv.NextRecord();
            }
        }

        public List<Movie> GetAll()
        {
            using var reader = new StreamReader(_filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            return csv.GetRecords<Movie>().ToList();
        }

        public void Add(Movie movie)
        {
            var movies = GetAll();

            // Auto-generate Id
            movie.Id = movies.Any() ? movies.Max(m => m.Id) + 1 : 1;

            movies.Add(movie);

            SaveAll(movies);
        }

        public void Update(Movie movie)
        {
            var movies = GetAll();
            var existing = movies.FirstOrDefault(m => m.Id == movie.Id);

            if (existing == null)
                throw new Exception("Movie not found");

            existing.Title = movie.Title;
            existing.Director = movie.Director;
            existing.DurationMinutes = movie.DurationMinutes;
            existing.Rating = movie.Rating;

            SaveAll(movies);
        }

        public void Delete(int id)
        {
            var movies = GetAll();
            var movie = movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
                throw new Exception("Movie not found");

            movies.Remove(movie);
            SaveAll(movies);
        }

        public void SaveAll(List<Movie> movies)
        {
            using var writer = new StreamWriter(_filePath, false); // overwrite
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteRecords(movies);
        }
    }
}
