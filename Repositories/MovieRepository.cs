using atelier3.Models;
using atelier3.Repositories.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System;

namespace TP3.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationdbContext _db;

        public MovieRepository(ApplicationdbContext db)
        {
            _db = db;
        }

        public List<Movie> GetAllMovies()
        {
            return _db.movies.ToList();
        }

        public Movie GetMovieById(int id)
        {
            return _db.movies.Find(id);
        }

        public void CreateMovie(Movie movie)
        {
            if (movie.ImageFile != null && movie.ImageFile.Length > 0)
            {
                var imagePath = Path.Combine("wwwroot/images", movie.ImageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    movie.ImageFile.CopyTo(stream);
                }

                movie.Photo = $"/images/{movie.ImageFile.FileName}";
            }

            _db.movies.Add(movie);
            _db.SaveChanges();
        }

        public void EditMovie(Movie movie)
        {
            _db.Entry(movie).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            var movie = _db.movies.Find(id);

            if (movie != null)
            {
                _db.movies.Remove(movie);
                _db.SaveChanges();
            }
        }

        public List<Movie> GetMoviesByGenre(int genreId)
        {
            return _db.movies
                .Where(m => m.GenreId == genreId)
                .ToList();
        }

        public List<Movie> GetAllMoviesOrderedAscending()
        {
            return _db.movies
                .OrderBy(m => m.Name)
                .ToList();
        }
        List<Movie> IMovieRepository.GetMoviesByUserDefinedGenre(string userDefinedGenre)
        {
            return _db.movies
                            .Where(m => m.Genres.Where(g => g.Name == userDefinedGenre) != null)
                            .ToList();
        }
    }
}

