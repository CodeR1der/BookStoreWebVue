using BookStoreWebVue.Server.BookStore;
using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookStoreWebVue.Server.DataAccess
{
    public class CountryDataAccess
    {
        private readonly string _connectionString;

        public CountryDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Country> GetCountryList() 
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allCountries = db.GetTable<Country>()
                                 .ToList();
                return allCountries;
            }
        }
        public void PrintAllCountries()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allCountries = db.GetTable<Country>().ToList();

                Console.WriteLine("{0,-10} {1,-20}", "Country ID", "Country Name");
                Console.WriteLine(new string('-', 30));

                foreach (var country in allCountries)
                {
                    Console.WriteLine("{0,-10} {1,-20}", country.countryId, country.countryName);
                }
            }
        }

        public void AddCountry(Country country)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(country);
            }
        }

        public Country GetCountryById(Guid countryId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<Country>().FirstOrDefault(c => c.countryId == countryId);
            }
        }

        public void UpdateCountry(Country country)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(country);
            }
        }

        public void DeleteCountry(Guid countryId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<Country>().Delete(c => c.countryId == countryId);
            }
        }
    }

}
