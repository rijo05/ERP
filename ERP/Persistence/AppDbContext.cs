using ERP.Domain.Entities;
using ERP.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ERP.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        

        //Cria as tabelas de acordo com as entities
        //Virtual pois permite ser mockado nos testes
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasConversion(v => v.Value, v => new Email(v))
                .HasColumnName("Email")
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasConversion(v => v.Amount, v => new Price(v))     
                .HasColumnName("Price")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}
