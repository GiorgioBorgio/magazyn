using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn.Entities
{
    internal class WarehouseDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<PasswordHistory> PasswordHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(eb =>
            {
                eb.HasMany(a => a.Users).WithOne(u => u.Address).HasForeignKey(e=>e.AddressId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<UserPermission>(eb =>
            {
                eb.HasMany(r => r.Users).WithOne(u => u.UserPermission).IsRequired().OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Permission>(eb =>
            {
                eb.HasMany(p => p.UserPermissions).WithOne(u => u.Permission).IsRequired().OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<PasswordHistory>(eb =>
            {
                eb.HasOne(e=>e.User).WithMany(u=>u.PasswordHistories).IsRequired().OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(eb =>
            {
                eb.HasOne(u => u.ForgotenBy) 
                  .WithMany(u => u.ForgottenUsers) 
                  .HasForeignKey(u => u.ForgottenById)
                  .OnDelete(DeleteBehavior.Restrict); 
            });


            //modelBuilder.Entity<Product>(eb => 
            //{
            //    eb.HasOne(p => p.RegisteringUser).WithMany(u => u.Products);
            //    eb.HasMany(e=> e.WarehouseHistory).WithOne(e=>e.Product).HasForeignKey(e=>e.ProductId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            //    eb.HasOne(e => e.StockLevel).WithOne(s => s.Product).HasForeignKey<StockLevel>(k => k.ProductId);
            //});
            //modelBuilder.Entity<ProductSale>(eb =>
            //{
            //    eb.HasOne(s => s.Product).WithMany(p => p.ProductSales).HasForeignKey(k=>k.ProductId).OnDelete(DeleteBehavior.NoAction);
            //});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Database=WarehouseDb;Integrated Security=True;Encrypt=True"); 
        }

    }
}
