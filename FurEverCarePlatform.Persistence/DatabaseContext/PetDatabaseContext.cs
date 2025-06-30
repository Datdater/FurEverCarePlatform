using FurEverCarePlatform.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Persistence.DatabaseContext;

public class PetDatabaseContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public PetDatabaseContext(DbContextOptions<PetDatabaseContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Domain.Entities.Order>().HasIndex(o => o.Code).IsUnique(false);

        modelBuilder.Entity<Booking>().HasIndex(b => b.Code).IsUnique(false);
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingDetail> BookingDetails { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Domain.Entities.Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Payment> Payments { get; set; }

    //public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Domain.Entities.PetService> PetServices { get; set; }
    public DbSet<PetServiceDetail> PetServiceDetails { get; set; }
    public DbSet<PetServiceStep> PetServiceSteps { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<ProductReviews> ProductReviews { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }

    public DbSet<ProductVariant> ProductVariations { get; set; }
}
