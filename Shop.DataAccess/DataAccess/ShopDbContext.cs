using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;

namespace Shop.DataAccess.DataAccess;

public class ShopDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=LAPTOP-8VT5QBPD\SQLEXPRESS;Database=ShopProject;Trusted_Connection=true");
    }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Wallet> Wallets { get; set; } = null!;
    public DbSet<Basket> Baskets { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<Discount> Discounts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<ShowInvoicesVW> ShowInvoicesVWs { get; set; } = null!;
    public DbSet<BasketProduct> BasketProducts { get; set; } = null!;
    public DbSet<ProductInvoice> ProductInvoices { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region One-TO-One

        modelBuilder.Entity<User>()
            .HasOne(u => u.Basket)
            .WithOne(b => b.User)
            .HasForeignKey<Basket>(b => b.UserId);
        #endregion

        #region one-to-many
        modelBuilder.Entity<User>()
            .HasMany(u => u.Wallet)
            .WithOne(w => w.User)
            .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Invoice)
            .WithOne(i => i.User)
            .HasForeignKey(i => i.UserId);

        modelBuilder.Entity<Discount>()
            .HasMany(d => d.Product)
            .WithOne(p => p.Discount)
            .HasForeignKey(p => p.DiscountId);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
        modelBuilder.Entity<Brand>()
           .HasMany(b => b.Products)
           .WithOne(p => p.Brand)
           .HasForeignKey(p => p.BrandId);

        #endregion
        #region many-to-many

        modelBuilder.Entity<BasketProduct>()
            .HasKey(bp => new { bp.ProductId, bp.BasketID });
        modelBuilder.Entity<Basket>()
            .HasMany(b => b.BasketProduct)
            .WithOne(bp => bp.Basket)
            .HasForeignKey(bp => bp.BasketID);
        modelBuilder.Entity<Product>()
            .HasMany(p => p.BasketProduct)
            .WithOne(bp => bp.Product)
            .HasForeignKey(bp => bp.ProductId);


        modelBuilder.Entity<ProductInvoice>()
            .HasKey(pi => new { pi.ProductId, pi.InvoiceId });
        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductInvoice)
            .WithOne(pi => pi.Product)
            .HasForeignKey(pi => pi.ProductId);
        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.ProductInvoice)
            .WithOne(pi => pi.Invoice)
            .HasForeignKey(pi => pi.InvoiceId);

        modelBuilder.Entity<ShowInvoicesVW>()
            .ToView("vw_Invoices")
            .HasNoKey();
        #endregion

    }


}
