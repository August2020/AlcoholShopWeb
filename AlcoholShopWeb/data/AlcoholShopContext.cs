﻿using AlcoholShop.Models;
using AlcoholShopWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AlcoholShopWeb.Data
{
    public class AlcoholShopContext : DbContext
    {
        public AlcoholShopContext(DbContextOptions<AlcoholShopContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<BlogPostTag> BlogPostTags { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<ProductionMethod> ProductionMethods { get; set; }
        public DbSet<Aging> Aging { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogPostTag>()
                .HasKey(bpt => new { bpt.PostID, bpt.TagID });

            modelBuilder.Entity<BlogPostTag>()
                .HasOne(bpt => bpt.Post)
                .WithMany(p => p.BlogPostTags)
                .HasForeignKey(bpt => bpt.PostID);

            modelBuilder.Entity<BlogPostTag>()
                .HasOne(bpt => bpt.Tag)
                .WithMany(t => t.BlogPostTags)
                .HasForeignKey(bpt => bpt.TagID);
        }
    }
}
