using Microsoft.EntityFrameworkCore;


namespace Assignment_3_Resume_Builder.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Comment> Comments { get; set; }
    }
}
