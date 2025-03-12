using FurEverCarePlatform.Domain.Entities;
using FurEverCarePlatform.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FurEverCarePlatform.Persistence.DatabaseContext;

public class PetDatabaseContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public PetDatabaseContext(DbContextOptions options) : base(options)
    {
    }
    public PetDatabaseContext() : base() 
    {

    }


    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("PetDB")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
        modelBuilder.ApplyConfiguration(new BookingConfiguration());
        modelBuilder.ApplyConfiguration(new BookingDetailConfiguration());
        modelBuilder.ApplyConfiguration(new BookingDetailServiceConfiguration());
        modelBuilder.ApplyConfiguration(new BreedConfiguration());
        modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
        modelBuilder.ApplyConfiguration(new ComboConfiguration());
        modelBuilder.ApplyConfiguration(new ComboServiceConfiguration());
		modelBuilder.ApplyConfiguration(new DeliveryConfiguration());
		modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
		modelBuilder.ApplyConfiguration(new HealthDetailConfiguration());
		modelBuilder.ApplyConfiguration(new NotificationConfiguration());
		modelBuilder.ApplyConfiguration(new OrderConfiguration());
		modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
		modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
		modelBuilder.ApplyConfiguration(new PaymentConfiguration());
		modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
		modelBuilder.ApplyConfiguration(new PetConfiguration());
		modelBuilder.ApplyConfiguration(new PetServiceConfiguration());
		modelBuilder.ApplyConfiguration(new PetServiceStepConfiguration());
		modelBuilder.ApplyConfiguration(new PetServiceDetailConfiguration());
		modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());
		modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
		modelBuilder.ApplyConfiguration(new ProductConfiguration());
		modelBuilder.ApplyConfiguration(new ProductPriceConfiguration());
		modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
		modelBuilder.ApplyConfiguration(new ProductTypeDetailConfiguration());
		modelBuilder.ApplyConfiguration(new PromotionConfiguration());
		modelBuilder.ApplyConfiguration(new ReportConfiguration());
		modelBuilder.ApplyConfiguration(new ServiceCategoryConfiguration());
		modelBuilder.ApplyConfiguration(new ServiceTrackingConfiguration());
		modelBuilder.ApplyConfiguration(new StoreConfiguration());
	}

    public DbSet<AppUser> AppUsers { get; set; }
	public DbSet<AppRole> AppRoles { get; set; }
	public DbSet<Address> Addresses { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingDetail> BookingDetails { get; set; }
    public DbSet<BookingDetailService> BookingDetailServices { get; set; }
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Combo> Combos { get; set; }
    public DbSet<ComboService> ComboServices { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<HealthDetail> HealthDetails { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<PetService> PetServices { get; set; }
    public DbSet<PetServiceDetail> PetServiceDetails { get; set; }
    public DbSet<PetServiceStep> PetServiceSteps { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductCategory> productCategories { get; set; }
    public DbSet<Product> Products { get; set; } 
    public DbSet<ProductPrice> ProductPrices { get; set; }
    public DbSet<ProductType> productTypes { get; set; }
    public DbSet<ProductTypeDetail> ProductTypeDetails { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<ServiceTracking> ServiceTrackings { get; set; }
    public DbSet<Store> Stores { get; set; }

}