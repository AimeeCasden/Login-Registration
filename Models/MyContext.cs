using Microsoft.EntityFrameworkCore;
 
namespace LoginRegistration.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }
        // "users" table is represented by this DbSet "Users"
        public DbSet<Register> Users{get;set;}
    }
}