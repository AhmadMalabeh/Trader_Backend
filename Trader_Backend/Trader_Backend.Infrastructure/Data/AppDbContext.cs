using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Trader_Backend.Domain.Entities;
using Trader_Backend.Infrastructure.ProjectConfigurations;
namespace Trader_Backend.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<OptionGroup> OptionGroups { get; set; } = null!;
        public DbSet<Option> Options { get; set; } = null!;
        public DbSet<Ad> Ads { get; set; } = null!;
        public DbSet<AdImage> AdImages { get; set; } = null!;
        public DbSet<UserFavoriteAd> UserFavoriteAds { get; set; } = null!;
        public DbSet<InvitationCode> InvitationCodes { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Apply all configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    
    }
}
