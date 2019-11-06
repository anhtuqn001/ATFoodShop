using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<ShoppingCartItem>()
                        .HasOne(p => p.ShoppingCart)
                        .WithMany(b => b.Items)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ShoppingCartItem>()
                        .HasOne(p => p.Food)
                        .WithMany(f => f.ShoppingCartItems)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Food>()
                    .Property(f => f.FoodId)
                        .ValueGeneratedOnAdd();
        }
    }
}
