using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    internal class DbUtils
    {
        private readonly string connectionString;

        public DbUtils(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public User AddUser(string login, string password, string name = null)
        {
            User user = new User()
            {
                ID = Guid.NewGuid(),
                Login = login,
                Password = password,
                Name = login
            };

            using (ShopContext db = new ShopContext(this.connectionString))
            {
                db.Users.Add(user);
                try
                {
                    db.SaveChanges();
                }
                catch { }
            }

            return user;
        }

        public User? FindUser(string login)
        {
            using (ShopContext db = new ShopContext(this.connectionString))
            {

                return (
                    from u in db.Users
                    where u.Login == login
                    select u
                    ).SingleOrDefault();
            }
        }

        public void AddProduct(string name, string description, int? price = null)
        {
            using (ShopContext db = new ShopContext(this.connectionString))
            {
                var product = new Product()
                {
                    ID = Guid.NewGuid(),
                    Name = name,
                    Description = description,
                    Price = price
                };
                db.Products.Add(product);

                try
                {
                    db.SaveChanges();
                }
                catch { }
            }
        }
    }
}
