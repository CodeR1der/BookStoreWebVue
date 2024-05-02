using BookStoreWebVue.Server.BookStore;
using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWebVue.Server.DataAccess
{
    public class BookDataAccess
    {
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _environment;

        public BookDataAccess(string connectionString, IWebHostEnvironment environment)
        {
            _connectionString = connectionString;
            _environment = environment;

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
                Console.WriteLine("{0,-5} {1,-10} {2, -25} {3, -30} {4, -45} {5, -50}", "Book ID", "Title", "Author", "Language", "Publisher", "Genre");
                Console.WriteLine(new string('-', 70));

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
                                 .OrderBy(book => book.title)
                                 .LoadWith(req => req.author)
                                 .LoadWith(req => req.language)
                                 .LoadWith(req => req.publisher)
                                 .LoadWith(req => req.genre)
                                 .ToList();
                return allBooks;
            }
        }

        public class BookWithCover
        {
            public Book Book { get; set; }
            public string CoverBase64 { get; set; }
        }

        public List<BookWithCover> GetAllBooksWithCover()
        {
            // Получаем список всех книг
            var allBooks = GetAllBooks();

            string imageFolderPath = Path.Combine(_environment.ContentRootPath, "ImageBooks");

            // Создаем список для хранения книг с base64 изображениями обложек
            var booksWithCoverBase64 = new List<BookWithCover>();

            // Для каждой книги в списке
            foreach (var book in allBooks)
            {
                string coverPath = Path.Combine(imageFolderPath, $"{book.bookId}.jpg");

                if (File.Exists(coverPath))
                {
                    // Если файл обложки существует, считываем его в байтовый массив
                    byte[] imageBytes = File.ReadAllBytes(coverPath);
                    // Преобразуем байтовый массив в base64 строку
                    string base64String = "data:image/jpeg;base64,"+Convert.ToBase64String(imageBytes);

                    // Добавляем книгу с base64 изображением обложки в список
                    booksWithCoverBase64.Add(new BookWithCover { Book = book, CoverBase64 = base64String });
                }
                else
                {
                    // Если файл обложки не существует, добавляем null
                    booksWithCoverBase64.Add(new BookWithCover { Book = book, CoverBase64 = null });
                }
            }

            return booksWithCoverBase64;
        }



        public void AddBook(Book book)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(book);
            }
        }

        public Book GetBookById(Guid bookId)
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

        public void DeleteBook(Guid bookId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<Book>().Delete(p => p.bookId == bookId);
            }
        }


    }
}
