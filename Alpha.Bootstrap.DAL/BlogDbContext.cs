using Alpha.Bootstrap.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.DAL
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> dbContextOptions): base(dbContextOptions)
        { }

        public DbSet<Post> Posts { get; set; }
    }
}
