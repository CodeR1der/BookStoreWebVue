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
    public class LanguageDataAccess
    {
        private readonly string _connectionString;

        public LanguageDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<BookLanguage> GetBookLanguages() 
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allLanguages = db.GetTable<BookLanguage>()
                                 .ToList();
                return allLanguages;
            }
        }
        public void PrintAlllanguages()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allLanguages = db.GetTable<BookLanguage>().ToList();

                Console.WriteLine("{0,-10} {1,-20} {2,-30}", "Language ID", "Language code", "Language");
                Console.WriteLine(new string('-', 30));

                foreach (var language in allLanguages)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-30}", language.languageId, language.languageCode, language.languageName);
                }
            }
        }

        public void AddLanguage(BookLanguage language)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(language);
            }
        }

        public BookLanguage GetLanguageIdById(int languageId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<BookLanguage>().FirstOrDefault(g => g.languageId == languageId);
            }
        }

        public void UpdateLanguage(BookLanguage language)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(language);
            }
        }

        public void DeleteLanguage(int languageId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<BookLanguage>().Delete(g => g.languageId == languageId);
            }
        }
    }
}
