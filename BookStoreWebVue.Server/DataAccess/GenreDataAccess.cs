using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore;
using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;

namespace TestOperations
{
    public class GenreDataAccess
    {
        private readonly string _connectionString;

        public GenreDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Genre> GetCGenres()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allGenres = db.GetTable<Genre>()
                                 .ToList();
                return allGenres;
            }
        }

        public void PrintAllGenres()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allGenres = db.GetTable<Genre>().ToList();

                Console.WriteLine("{0,-10} {1,-20}", "Genre ID", "Genre Name");
                Console.WriteLine(new string('-', 30));

                foreach (var genre in allGenres)
                {
                    Console.WriteLine("{0,-10} {1,-20}", genre.genreId, genre.genreName);
                }
            }
        }

        public void AddGenre(Genre genre)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(genre);
            }
        }

        public Genre GetGenreById(int genreId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<Genre>().FirstOrDefault(g => g.genreId == genreId);
            }
        }

        public void UpdateGenre(Genre genre)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(genre);
            }
        }

        public void DeleteGenre(int genreId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<Genre>().Delete(g => g.genreId == genreId);
            }
        }
    }
}
