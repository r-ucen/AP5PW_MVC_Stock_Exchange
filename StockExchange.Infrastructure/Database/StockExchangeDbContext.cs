using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockExchange.Domain.Entities;
using StockExchange.Infrastructure.Database.Seeding;
using StockExchange.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Infrastructure.Database
{
    public class StockExchangeDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioStock> PortfolioStocks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Market> Markets { get; set; }

        public StockExchangeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // map the user relationships
            modelBuilder.Entity<Order>()
                .HasOne<User>(e => e.User as User)
                .WithMany()
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Transaction>()
                .HasOne<User>(e => e.User as User)
                .WithMany()
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Portfolio>()
                .HasOne<User>(e => e.User as User)
                .WithOne()
                .HasForeignKey<Portfolio>(e => e.UserId);

            // seeding Entities
            StockInit stockInit = new StockInit();
            modelBuilder.Entity<Stock>().HasData(stockInit.getStocks());

            OrderInit orderInit = new OrderInit();
            modelBuilder.Entity<Order>().HasData(orderInit.GetOrders());

            TransactionInit transactionInit = new TransactionInit();
            modelBuilder.Entity<Transaction>().HasData(transactionInit.GetTransactions());

            PortfolioInit portfolioInit = new PortfolioInit();
            modelBuilder.Entity<Portfolio>().HasData(portfolioInit.getPortfolio());

            PortfolioStockInit portfolioStockInit = new PortfolioStockInit();
            modelBuilder.Entity<PortfolioStock>().HasData(portfolioStockInit.GetPortfolioStocks());

            MarketInit marketInit = new MarketInit();
            modelBuilder.Entity<Market>().HasData(marketInit.GetMarkets());

            // seeding Identity
            // Identity - User and Role init
            RolesInit rolesInit = new RolesInit();
            modelBuilder.Entity<Role>().HasData(rolesInit.GetRolesAMC());

            // create users
            UserInit userInit = new UserInit();
            User admin = userInit.GetAdmin();
            User manager = userInit.GetManager();

            // add users to the table
            modelBuilder.Entity<User>().HasData(admin, manager);

            // connect the users with the roles
            UserRolesInit userRolesInit = new UserRolesInit();
            List<IdentityUserRole<int>> adminUserRoles = userRolesInit.GetRolesForAdmin();
            List<IdentityUserRole<int>> managerUserRoles = userRolesInit.GetRolesForManager();
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(adminUserRoles);
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(managerUserRoles);
        }
    }
}
