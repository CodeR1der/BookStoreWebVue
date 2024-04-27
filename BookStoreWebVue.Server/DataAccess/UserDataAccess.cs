using BookStoreWebVue.Server.BookStore;
using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;

namespace BookStoreWebVue.Server.DataAccess
{
    public class UserDataAccess
    {
        private readonly string _connectionString;

        public UserDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<User> GetUsers()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allUsers = db.GetTable<User>()
                    .LoadWith(u =>u.userId)
                                 .ToList();
                return allUsers;
            }
        }

        public void AddUser(User user)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(user);
            }
        }

        public User GetUserById(Guid userId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<User>().FirstOrDefault(a => a.userId == userId);
            }
        }
        public User GetUserByEmail(string userEmail)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<User>().FirstOrDefault(a => a.email == userEmail);
            }
        }

        public void UpdateUser(User user)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(user);
            }
        }

        public void DeleteUser(Guid userId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<User>().Delete(a => a.userId == userId);
            }
        }
    }
}
