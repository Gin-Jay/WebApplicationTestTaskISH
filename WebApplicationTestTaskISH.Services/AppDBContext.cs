using Microsoft.EntityFrameworkCore;
using WebApplicationTestTaskISH.Models;

namespace WebApplicationTestTaskISH.Services
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
