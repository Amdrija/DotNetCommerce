using System.Linq;
using Logeecom.DemoProjekat.DAL.Models;

namespace Logeecom.DemoProjekat.DAL.Repositories
{
    public class UserRepository
    {
        private readonly DemoDbContext db;

        public UserRepository(DemoDbContext db)
        {
            this.db = db;
        }

        public void CreateAdmin(User admin)
        {
            if (!this.db.Users.Any())
            {
                this.db.Users.Add(admin);
                this.db.SaveChanges();
            }
        }

        public User FindUser(string username)
        {
            return this.db.Users.Where(user => user.Username == username).FirstOrDefault();
        }
    }
}
