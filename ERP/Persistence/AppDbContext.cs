using ERP.Domain.Common;
using ERP.Domain.Entities;
using ERP.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.OwnsOne(u => u.Email, email =>
                {
                    email.Property(e => e.Value)
                        .HasColumnName("Email")
                        .IsRequired();
                });

                entity.OwnsOne(u => u.Role, role =>
                {
                    role.Property(r => r.roleName)
                        .HasConversion<string>()
                        .HasColumnName("Role")
                        .IsRequired();
                });
            });

            modelBuilder.Entity<Product>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Email>().ComplexProperty(v => v.Value);
        }

        public List<IDomainEvent> GetAllDomainEvents()
        {
            return ChangeTracker
                .Entries<IHasDomainEvents>()
                .SelectMany(e => e.Entity.DomainEvents)
                .ToList();
        }

        public void ClearAllDomainEvents()
        {
            var domainEntities = ChangeTracker
                .Entries<IHasDomainEvents>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity);

            foreach (var entity in domainEntities)
                entity.ClearDomainEvents();
        }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
