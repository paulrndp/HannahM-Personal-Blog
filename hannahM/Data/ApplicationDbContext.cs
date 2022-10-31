using hannahM.Models;
using Microsoft.EntityFrameworkCore;

namespace hannahM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Chapters> Chapter { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Posts> Post { get; set; }
        public DbSet<Visitor> Visitor { get; set; }
        public DbSet<Gallery> Galleries { get; set; }

    }
}
