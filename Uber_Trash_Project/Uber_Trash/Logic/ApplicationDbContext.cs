using Microsoft.EntityFrameworkCore;
using Uber_Trash.Model;

namespace Uber_Trash.Logic
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<KeyPairModel> KeyPairModel { get; set; }
        public DbSet<UserSellers> Sellers { get; set; }
    }
}
