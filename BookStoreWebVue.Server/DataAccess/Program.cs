using System;
using System.Linq;
using BookStore;
using LinqToDB;
using LinqToDB.Data;
using TestOperations;
using LinqToDB.DataProvider.PostgreSQL;

namespace MyApp 
{
    internal class Program
    {
        static void ain(string[] args)
        {
            var connectionString = "Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore";
            BookDataAccess customerDataAccess = new BookDataAccess(connectionString);
            AddressDataAccess addressDataAccess = new AddressDataAccess(connectionString);
            addressDataAccess.PrintAllAddresses();
            addressDataAccess.DeleteAddress(1);
            addressDataAccess.DeleteAddress(2);
            addressDataAccess.DeleteAddress(3);
            addressDataAccess.DeleteAddress(4);
            addressDataAccess.DeleteAddress(5);
            addressDataAccess.PrintAllAddresses();


            //Вывод всей таблицы
            customerDataAccess.PrintAllBooks();

            LanguageDataAccess languageDataAccess = new LanguageDataAccess(connectionString);
            languageDataAccess.PrintAlllanguages();

            BookLanguage bookLanguage = new BookLanguage { languageId = 4, languageCode = "ru", languageName = "Russian" };
            languageDataAccess.AddLanguage(bookLanguage);
            languageDataAccess.PrintAlllanguages();

            languageDataAccess.DeleteLanguage(4);
            languageDataAccess.PrintAlllanguages();

            //Добавление записи
            Book newBookAvailability = new Book { title = "The Woman in Me", isbn13 = "9781398522527", authorId = 2, languageId = 1, numPages = 277, publicationDate = DateTime.Parse("2023-04-30"), publisherId = 4, genreId = 5 };
            customerDataAccess.AddBook(newBookAvailability);
            customerDataAccess.PrintAllBooks();

            // Получение записи по идентификатору
            Book retrievedCustomer = customerDataAccess.GetBookById(6);
            Console.WriteLine($"Book availability: {retrievedCustomer.title} {retrievedCustomer.publisherId} {retrievedCustomer.authorId}");

            // Изменение записи
            retrievedCustomer.publisherId = 5;
            customerDataAccess.UpdateBook(retrievedCustomer);

            // Получение измененной записи
            retrievedCustomer = customerDataAccess.GetBookById(6);
            Console.WriteLine($"new book availability: {retrievedCustomer.title} {retrievedCustomer.publisher.publisherName} {retrievedCustomer.author.authorName}");

            // Удаление записи
            customerDataAccess.DeleteBook(6);
            Console.WriteLine("Book deleted.");
            customerDataAccess.PrintAllBooks();

        }
    }
}