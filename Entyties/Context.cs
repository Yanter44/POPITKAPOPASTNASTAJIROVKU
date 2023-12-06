using Microsoft.EntityFrameworkCore;


namespace POPITKAPOPASTNASTAJIROVKU.Entyties
{
    public class Context : DbContext
    {
        public DbSet<Book> Books {get; set;}
 
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
            Database.OpenConnection();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }
    
       
    }
}
