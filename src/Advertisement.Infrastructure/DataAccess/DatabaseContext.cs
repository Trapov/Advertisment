using System;
using System.Xml.Xsl;
using Advertisement.Domain;
using Microsoft.EntityFrameworkCore;

namespace Advertisement.Infrastructure.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<Ad> Ads { get; set; }
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ad>(builder =>
            {
                builder.HasKey(x => x.Id);
                
                builder.Property(x => x.CreatedAt).IsRequired();
                builder.Property(x => x.UpdatedAt).IsRequired(false);
                
                builder.Property(x => x.Name).HasMaxLength(100).IsUnicode();
                builder.Property(x => x.Price).HasColumnType("money");

                builder.Property(x => x.Status)
                    .HasConversion<string>(s => s.ToString(), s => Enum.Parse<Ad.Statuses>(s));

                builder.HasOne(x => x.Owner)
                    .WithMany()
                    .HasForeignKey(s => s.OwnerId)
                    .HasPrincipalKey(u => u.Id);
            });
            
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(x => x.Id);
                
                builder.Property(x => x.CreatedAt).IsRequired();
                builder.Property(x => x.UpdatedAt).IsRequired(false);

                builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                builder.Property(x => x.Password)
                    .IsRequired();
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}