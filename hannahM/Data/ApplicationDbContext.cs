using hannahM.Models;
using Microsoft.EntityFrameworkCore;

namespace hannahM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<RandomThoughts> Random { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Chapters> Chapter { get; set; }
        public DbSet<Account> Accounts { get; set; }



    }
}
