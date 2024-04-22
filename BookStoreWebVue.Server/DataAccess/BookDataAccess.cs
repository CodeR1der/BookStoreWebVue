using BookStore;
using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOperations
{
    public class BookDataAccess
    {
        private readonly string _connectionString;

        public BookDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void PrintAllBooks()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allBooks = db.GetTable<Book>()
                                 .OrderBy(book => book.bookId)
                                 .LoadWith(req => req.author)
                                 .LoadWith(req => req.language)
                                 .LoadWith(req => req.publisher)
                                 .LoadWith(req => req.genre)
                                 .ToList();
                Console.WriteLine("{0,-5} {1,-10} {2, -25} {3, -30} {4, -45} {5, -50}", "Book ID", "Title", "Author" ,"Language","Publisher", "Genre");
                Console.WriteLine(new string('-',70));

                foreach (var book in allBooks)
                {
                    Console.WriteLine("{0,-5} {1,-10} {2, -25} {3, -30} {4, -45} {5, -50}", book.bookId, book.title, book.author.authorName, book.language.languageName, book.publisher.publisherName, book.genre.genreName);
                }
            }
        }
        public List<Book> GetAllBooks()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allBooks = db.GetTable<Book>()
                                 .OrderBy(book => book.bookId)
                                 .LoadWith(req => req.author)
                                 .LoadWith(req => req.language)
                                 .LoadWith(req => req.publisher)
                                 .LoadWith(req => req.genre)
                                 .ToList();
                return allBooks;
            }
        }
        public void AddBook(Book book)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(book);
            }
        }

        public Book GetBookById(int bookId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<Book>().FirstOrDefault(p => p.bookId == bookId);
            }
        }

        public void UpdateBook(Book book)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(book);
            }
        }

        public void DeleteBook(int bookId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<Book>().Delete(p => p.bookId == bookId);
            }
        }
    }
}
