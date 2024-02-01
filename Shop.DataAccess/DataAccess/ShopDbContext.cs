using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;

namespace Shop.DataAccess.DataAccess;

public class ShopDbContext:DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=LAPTOP-8VT5QBPD\SQLEXPRESS;Database=ShopProject;Trusted_Connection=true");
    }
    DbSet<User> Users { get; set; } = null!;
    DbSet<Wallet> Wallets { get; set; } = null!;
    DbSet<Basket> Baskets { get; set; } = null!;
    DbSet<Product> Products { get; set; } = null!;
    DbSet<Invoice> Invoices { get; set; } = null!;
    DbSet<Discount> Discounts { get; set; } = null !;
    DbSet<BasketProduct> BasketProducts { get; set; } = null!;
    DbSet<ProductInvoice> ProductInvoices { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }


}
