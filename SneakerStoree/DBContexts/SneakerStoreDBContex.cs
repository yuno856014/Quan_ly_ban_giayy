using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SneakerStoree.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerStoree.DBContexts
{
    public class SneakerStoreDBContex : IdentityDbContext<AppIdentityUser>
    {

        public SneakerStoreDBContex(DbContextOptions<SneakerStoreDBContex> options) : base(options)
        {
        }
        public DbSet<Sneaker> Sneakers { get; set; }
        public DbSet<TradeMark> TradeMarks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedingTradeMark(modelBuilder);
            SeedingSneaker(modelBuilder);
            SeedingAspNetUser(modelBuilder);
            SeedingAspNetRole(modelBuilder);
            SeedingAspNetUserRole(modelBuilder);
        }
        private void SeedingTradeMark(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TradeMark>().HasData(
                new TradeMark()
                {
                    TradeMarkId = 1,
                    TradeMarkName = "Converse",
                    IsDeleted = false
                },
                new TradeMark()
                {
                    TradeMarkId = 2,
                    TradeMarkName = "Nike",
                    IsDeleted = false
                },
                new TradeMark()
                {
                    TradeMarkId = 3,
                    TradeMarkName = "Adidas",
                    IsDeleted = false
                },
                new TradeMark()
                {
                    TradeMarkId = 4,
                    TradeMarkName = "Vans",
                    IsDeleted = false
                }
                );
        }
        private void SeedingSneaker(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sneaker>().HasData(
                new Sneaker()
                {
                    SneakerId = 1,
                    SneakerName = "Vans Old Skool Black White",
                    Size = 42,
                    Quantity = 10,
                    Photo = "images/vans_b-w.jpg",
                    Price = 1350000,
                    PublishYear = 2019,
                    TradeMarkId = 4,
                    IsDeleted = false,
                    Information = "Giày Đẹp"
                },
                new Sneaker()
                {
                    SneakerId = 2,
                    SneakerName = "Converse 1970s Chuck Taylor 2",
                    Size = 42,
                    Quantity = 10,
                    Photo = "images/converse-w.jpg",
                    Price = 715000,
                    PublishYear = 2019,
                    TradeMarkId = 1,
                    IsDeleted = false,
                    Information = "Giày Đẹp"
                }
                );
        }
        private void SeedingAspNetUser(ModelBuilder modelBuilder)
        {
            AppIdentityUser user = new AppIdentityUser()
            {
                Id = "2c0fca4e-9376-4a70-bcc6-35bebe497866",
                UserName = "buu.nguyen@gmail.com",
                Email = "buu.nguyen@gmail.com",
                NormalizedEmail = "buu.nguyen@gmail.com",
                NormalizedUserName = "buu.nguyen@gmail.com",
                LockoutEnabled = false,
                Avatar = "/images/1.png"
            };
            PasswordHasher<AppIdentityUser> passwordHasher = new PasswordHasher<AppIdentityUser>();
            var passwordHash = passwordHasher.HashPassword(user, "Asdf1234!");
            user.PasswordHash = passwordHash;

            modelBuilder.Entity<AppIdentityUser>().HasData(user);
        }
        private void SeedingAspNetRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "c0c6661b-0964-4e62-8083-3cac6a6741ec",
                    Name = "SystemAdmin",
                    NormalizedName = "SystemAdmin",
                    ConcurrencyStamp = "1"
                },
                new IdentityRole()
                {
                    Id = "32ffd287-205f-43a2-9f0d-80sc5309fb47",
                    Name = "Customer",
                    NormalizedName = "Customer",
                    ConcurrencyStamp = "2"
                });
        }
        private void SeedingAspNetUserRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "c0c6661b-0964-4e62-8083-3cac6a6741ec",
                    UserId = "2c0fca4e-9376-4a70-bcc6-35bebe497866"
                });
        }
    }
}
