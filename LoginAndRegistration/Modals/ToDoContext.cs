using Microsoft.EntityFrameworkCore;
namespace LoginAndRegistration.Modals
{
    public class ToDoContext:DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options):base(options)
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Physician> Physicians { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Claim> Claims { get; set; }
    }
}
