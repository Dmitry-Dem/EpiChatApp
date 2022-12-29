using Microsoft.EntityFrameworkCore;

namespace EpiChatApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    
        //public DbSet<>
    }
}
