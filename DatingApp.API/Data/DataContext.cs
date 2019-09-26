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
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>()
                .HasKey(k => new {k.LikerId, k.LikeeId});

            builder.Entity<Like>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(u => u.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}