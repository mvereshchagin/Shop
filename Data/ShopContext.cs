using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ShopContext : DbContext
    {
        private static bool IsRecreated = false;

        private string connectionString;

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        public ShopContext(string connectionString) // : base(_CreateOptions(connectionString))
        {
            this.connectionString = connectionString;
            if (!IsRecreated)
            {
                this.Database.EnsureDeleted();
                IsRecreated = true;
            }
            this.Database.EnsureCreated();
        }

        static DbContextOptions<ShopContext> _CreateOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            return options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.
                UseSqlServer(this.connectionString);
        }

    }
}
