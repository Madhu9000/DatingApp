using Microsoft.EntityFrameworkCore;
using DatingApp.API.Model;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> optiona) : base(optiona)
        {

        }
        public DbSet<Value> Values { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<Photo> Photos { get; set; }
    }
}