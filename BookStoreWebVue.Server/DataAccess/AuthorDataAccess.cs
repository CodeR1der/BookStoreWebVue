using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStoreWebVue.Server.BookStore;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;

namespace BookStoreWebVue.Server.DataAccess
{
    public class AuthorDataAccess
    {
        private readonly string _connectionString;

        public AuthorDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Author> GetAuthors() 
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allAuthors = db.GetTable<Author>()
                                 .ToList();
                return allAuthors;
            }
        }
        public void PrintAllAuthors()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allAuthors = db.GetTable<Author>().ToList();

                Console.WriteLine("{0,-10} {1,-20}", "Author ID", "Author Name");
                Console.WriteLine(new string('-', 30));

                foreach (var author in allAuthors)
                {
                    Console.WriteLine("{0,-10} {1,-20}", author.authorId, author.authorName);
                }
            }
        }

        public void AddAuthor(Author author)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(author);
            }
        }

        public Author GetAuthorById(Guid authorId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<Author>().FirstOrDefault(a => a.authorId == authorId);
            }
        }

        public void UpdateAuthor(Author author)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(author);
            }
        }

        public void DeleteAuthor(Guid authorId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<Author>().Delete(a => a.authorId == authorId);
            }
        }
    }
}
