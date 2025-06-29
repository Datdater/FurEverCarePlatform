using System.Text.Json;
using FurEverCarePlatform.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Persistence;

public static class SeedData
{
    public static async Task SeedAsync(PetDatabaseContext context, UserManager<AppUser> userManager)
    {
        // Seed Product Categories
        await SeedProductCategories(context);

        // Seed Users
        await SeedUsers(userManager);

        // Seed Addresses
        await SeedAddresses(context);

        // Seed Stores
        await SeedStores(context);

        // Seed Products
        await SeedProducts(context);

        await context.SaveChangesAsync();
    }

    private static async Task SeedProductCategories(PetDatabaseContext context)
    {
        if (await context.ProductCategories.AnyAsync())
            return;

        var categories = new List<ProductCategory>
        {
            // Parent Categories
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Thức Ăn Thú Cưng",
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Đồ Chơi Thú Cưng",
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Chăm Sóc Sức Khỏe",
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Chăm Sóc Lông",
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Phụ Kiện Thú Cưng",
                CreationDate = DateTime.UtcNow,
            },
        };

        await context.ProductCategories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        // Get the created categories for sub-categories
        var petFoodCategory = await context.ProductCategories.FirstAsync(c =>
            c.Name == "Thức Ăn Thú Cưng"
        );
        var petToysCategory = await context.ProductCategories.FirstAsync(c =>
            c.Name == "Đồ Chơi Thú Cưng"
        );
        var petHealthCategory = await context.ProductCategories.FirstAsync(c =>
            c.Name == "Chăm Sóc Sức Khỏe"
        );
        var petGroomingCategory = await context.ProductCategories.FirstAsync(c =>
            c.Name == "Chăm Sóc Lông"
        );
        var petAccessoriesCategory = await context.ProductCategories.FirstAsync(c =>
            c.Name == "Phụ Kiện Thú Cưng"
        );

        var subCategories = new List<ProductCategory>
        {
            // Pet Food Sub-categories
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Thức Ăn Chó",
                ParentCategoryId = petFoodCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Thức Ăn Mèo",
                ParentCategoryId = petFoodCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Thức Ăn Chim",
                ParentCategoryId = petFoodCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Thức Ăn Cá",
                ParentCategoryId = petFoodCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            // Pet Toys Sub-categories
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Đồ Chơi Chó",
                ParentCategoryId = petToysCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Đồ Chơi Mèo",
                ParentCategoryId = petToysCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Đồ Chơi Tương Tác",
                ParentCategoryId = petToysCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            // Pet Health Sub-categories
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Vitamin & Bổ Sung",
                ParentCategoryId = petHealthCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Thuốc Thú Y",
                ParentCategoryId = petHealthCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Sơ Cứu",
                ParentCategoryId = petHealthCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            // Pet Grooming Sub-categories
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Sữa Tắm",
                ParentCategoryId = petGroomingCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Lược Chải Lông",
                ParentCategoryId = petGroomingCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Chăm Sóc Móng",
                ParentCategoryId = petGroomingCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            // Pet Accessories Sub-categories
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Vòng Cổ & Dây Xích",
                ParentCategoryId = petAccessoriesCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Giường & Chuồng",
                ParentCategoryId = petAccessoriesCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
            new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Bát & Máy Ăn",
                ParentCategoryId = petAccessoriesCategory.Id,
                CreationDate = DateTime.UtcNow,
            },
        };

        await context.ProductCategories.AddRangeAsync(subCategories);
        await context.SaveChangesAsync();
    }

    private static async Task SeedUsers(UserManager<AppUser> userManager)
    {
        if (await userManager.Users.AnyAsync())
            return;

        var users = new List<AppUser>
        {
            new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = "chuhang1@furevercare.com",
                Email = "chuhang1@furevercare.com",
                EmailConfirmed = true,
                PhoneNumber = "0901234567",
                PhoneNumberConfirmed = true,
                Name = "Nguyễn Văn An",
                CreationDate = DateTime.UtcNow,
            },
            new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = "chuhang2@furevercare.com",
                Email = "chuhang2@furevercare.com",
                EmailConfirmed = true,
                PhoneNumber = "0912345678",
                PhoneNumberConfirmed = true,
                Name = "Trần Thị Bình",
                CreationDate = DateTime.UtcNow,
            },
            new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = "chuhang3@furevercare.com",
                Email = "chuhang3@furevercare.com",
                EmailConfirmed = true,
                PhoneNumber = "0923456789",
                PhoneNumberConfirmed = true,
                Name = "Lê Văn Cường",
                CreationDate = DateTime.UtcNow,
            },
        };

        foreach (var user in users)
        {
            await userManager.CreateAsync(user, "Password123!");
        }
    }

    private static async Task SeedAddresses(PetDatabaseContext context)
    {
        if (await context.Addresses.AnyAsync())
            return;

        var users = await context.Users.Take(3).ToListAsync();

        var addresses = new List<Address>
        {
            new Address
            {
                Id = Guid.NewGuid(),
                Phone = "0901234567",
                Street = "123 Đường Nguyễn Huệ",
                City = "Thành phố Hồ Chí Minh",
                Province = "TP. Hồ Chí Minh",
                PostalCode = 70000,
                CoordinateX = 10.7769,
                CoordinateY = 106.7009,
                AppUserId = users[0].Id,
                CreationDate = DateTime.UtcNow,
            },
            new Address
            {
                Id = Guid.NewGuid(),
                Phone = "0912345678",
                Street = "456 Đường Lê Lợi",
                City = "Thành phố Hồ Chí Minh",
                Province = "TP. Hồ Chí Minh",
                PostalCode = 70000,
                CoordinateX = 10.8231,
                CoordinateY = 106.6297,
                AppUserId = users[1].Id,
                CreationDate = DateTime.UtcNow,
            },
            new Address
            {
                Id = Guid.NewGuid(),
                Phone = "0923456789",
                Street = "789 Đường Trần Hưng Đạo",
                City = "Thành phố Hồ Chí Minh",
                Province = "TP. Hồ Chí Minh",
                PostalCode = 70000,
                CoordinateX = 10.7769,
                CoordinateY = 106.7009,
                AppUserId = users[2].Id,
                CreationDate = DateTime.UtcNow,
            },
        };

        await context.Addresses.AddRangeAsync(addresses);
        await context.SaveChangesAsync();
    }

    private static async Task SeedStores(PetDatabaseContext context)
    {
        if (await context.Stores.AnyAsync())
            return;

        var users = await context.Users.Take(3).ToListAsync();
        var addresses = await context.Addresses.Take(3).ToListAsync();

        var stores = new List<Store>
        {
            new Store
            {
                Id = Guid.NewGuid(),
                Name = "Cửa Hàng Thú Cưng Hạnh Phúc",
                AddressId = addresses[0].Id,
                Hotline = "1900-1234",
                LogoUrl = "https://example.com/logos/hanh-phuc-logo.jpg",
                BannerUrl = "https://example.com/banners/hanh-phuc-banner.jpg",
                BusinessType = "Bán Lẻ Thức Ăn & Phụ Kiện Thú Cưng",
                BusinessAddressProvince = "TP. Hồ Chí Minh",
                BusinessAddressDistrict = "Quận 1",
                BusinessAddressWard = "Phường Bến Nghé",
                BusinessAddressStreet = "123 Đường Nguyễn Huệ",
                FaxEmail = "info@hanhphucpet.com",
                FaxCode = "HP001",
                FrontIdentityCardUrl = "https://example.com/documents/front-id-1.jpg",
                BackIdentityCardUrl = "https://example.com/documents/back-id-1.jpg",
                AppUserId = users[0].Id,
                CreationDate = DateTime.UtcNow,
            },
            new Store
            {
                Id = Guid.NewGuid(),
                Name = "Siêu Thị Thú Cưng Minh Châu",
                AddressId = addresses[1].Id,
                Hotline = "1900-5678",
                LogoUrl = "https://example.com/logos/minh-chau-logo.jpg",
                BannerUrl = "https://example.com/banners/minh-chau-banner.jpg",
                BusinessType = "Chăm Sóc & Cung Cấp Thú Cưng",
                BusinessAddressProvince = "TP. Hồ Chí Minh",
                BusinessAddressDistrict = "Quận 3",
                BusinessAddressWard = "Phường Võ Thị Sáu",
                BusinessAddressStreet = "456 Đường Lê Lợi",
                FaxEmail = "contact@minhchaupet.com",
                FaxCode = "MC002",
                FrontIdentityCardUrl = "https://example.com/documents/front-id-2.jpg",
                BackIdentityCardUrl = "https://example.com/documents/back-id-2.jpg",
                AppUserId = users[1].Id,
                CreationDate = DateTime.UtcNow,
            },
            new Store
            {
                Id = Guid.NewGuid(),
                Name = "Cửa Hàng Thú Cưng Cao Cấp PetCare",
                AddressId = addresses[2].Id,
                Hotline = "1900-9012",
                LogoUrl = "https://example.com/logos/petcare-logo.jpg",
                BannerUrl = "https://example.com/banners/petcare-banner.jpg",
                BusinessType = "Sản Phẩm Thú Cưng Cao Cấp",
                BusinessAddressProvince = "TP. Hồ Chí Minh",
                BusinessAddressDistrict = "Quận 7",
                BusinessAddressWard = "Phường Tân Phú",
                BusinessAddressStreet = "789 Đường Trần Hưng Đạo",
                FaxEmail = "hello@petcare.com",
                FaxCode = "PC003",
                FrontIdentityCardUrl = "https://example.com/documents/front-id-3.jpg",
                BackIdentityCardUrl = "https://example.com/documents/back-id-3.jpg",
                AppUserId = users[2].Id,
                CreationDate = DateTime.UtcNow,
            },
        };

        await context.Stores.AddRangeAsync(stores);
        await context.SaveChangesAsync();
    }

    private static async Task SeedProducts(PetDatabaseContext context)
    {
        if (await context.Products.AnyAsync())
            return;

        var stores = await context.Stores.ToListAsync();
        var dogFoodCategory = await context.ProductCategories.FirstAsync(c =>
            c.Id == Guid.Parse("973ca84f-3729-4d66-948d-60824d3b1784")
        );
        var catFoodCategory = await context.ProductCategories.FirstAsync(c =>
            c.Id == Guid.Parse("d5da4878-ea8a-44d1-a453-425fdc2cd95c")
        );
        var dogToysCategory = await context.ProductCategories.FirstAsync(c =>
            c.Id == Guid.Parse("e1602d59-80a1-4ee2-844d-20a425cc6939")
        );
        var catToysCategory = await context.ProductCategories.FirstAsync(c =>
            c.Id == Guid.Parse("888cdb98-ff49-411e-bdda-3aea17d93342")
        );
        var collarsCategory = await context.ProductCategories.FirstAsync(c =>
            c.Id == Guid.Parse("e11c4d8a-0707-446e-a95a-078342a656ee")
        );

        var products = new List<Product>
        {
            // Dog Food Products
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = dogFoodCategory.Id,
                StoreId = stores[0].Id,
                Name = "Thức Ăn Chó Cao Cấp - Gà & Gạo Lứt",
                Description =
                    "Thức ăn chó chất lượng cao được làm từ thịt gà thật và gạo lứt. Hoàn hảo cho chó trưởng thành.",
                BasePrice = 250000m,
                Weight = 2.5m,
                Length = 30.0m,
                Height = 15.0m,
                Sold = 150,
                StarAverage = 4.5,
                ReviewCount = 45,
                TotalRating = 202,
                CreationDate = DateTime.UtcNow,
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = dogFoodCategory.Id,
                StoreId = stores[1].Id,
                Name = "Thức Ăn Chó Không Ngũ Cốc - Cá Hồi",
                Description =
                    "Thức ăn chó không chứa ngũ cốc với cá hồi làm protein chính. Lý tưởng cho chó có dạ dày nhạy cảm.",
                BasePrice = 320000m,
                Weight = 3.0m,
                Length = 35.0m,
                Height = 18.0m,
                Sold = 89,
                StarAverage = 4.8,
                ReviewCount = 23,
                TotalRating = 110,
                CreationDate = DateTime.UtcNow,
            },
            // Cat Food Products
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = catFoodCategory.Id,
                StoreId = stores[0].Id,
                Name = "Thức Ăn Mèo Cao Cấp - Cá Ngừ & Cá Hồi",
                Description =
                    "Thức ăn mèo ngon miệng với cá ngừ và cá hồi. Giàu axit béo omega-3 cho da và lông khỏe mạnh.",
                BasePrice = 180000m,
                Weight = 1.5m,
                Length = 25.0m,
                Height = 12.0m,
                Sold = 234,
                StarAverage = 4.6,
                ReviewCount = 67,
                TotalRating = 308,
                CreationDate = DateTime.UtcNow,
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = catFoodCategory.Id,
                StoreId = stores[2].Id,
                Name = "Thức Ăn Mèo Hữu Cơ - Gà & Rau Củ",
                Description =
                    "Thức ăn mèo hữu cơ làm từ gà thả vườn và rau củ tươi. Không có chất bảo quản nhân tạo.",
                BasePrice = 280000m,
                Weight = 2.0m,
                Length = 28.0m,
                Height = 14.0m,
                Sold = 76,
                StarAverage = 4.9,
                ReviewCount = 34,
                TotalRating = 166,
                CreationDate = DateTime.UtcNow,
            },
            // Dog Toys
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = dogToysCategory.Id,
                StoreId = stores[1].Id,
                Name = "Đồ Chơi Thông Minh Cho Chó - Puzzle",
                Description =
                    "Đồ chơi puzzle kích thích trí não giúp chó của bạn thông minh hơn và giải trí hàng giờ.",
                BasePrice = 150000m,
                Weight = 0.5m,
                Length = 20.0m,
                Height = 8.0m,
                Sold = 189,
                StarAverage = 4.7,
                ReviewCount = 56,
                TotalRating = 263,
                CreationDate = DateTime.UtcNow,
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = dogToysCategory.Id,
                StoreId = stores[2].Id,
                Name = "Đồ Chơi Gặm Nhai Bền - Bóng Dây Thừng",
                Description =
                    "Bóng dây thừng siêu bền cho chó gặm nhai mạnh. Tốt cho sức khỏe răng miệng và thỏa mãn bản năng gặm nhai.",
                BasePrice = 120000m,
                Weight = 0.3m,
                Length = 15.0m,
                Height = 15.0m,
                Sold = 312,
                StarAverage = 4.4,
                ReviewCount = 89,
                TotalRating = 392,
                CreationDate = DateTime.UtcNow,
            },
            // Cat Toys
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = catToysCategory.Id,
                StoreId = stores[0].Id,
                Name = "Đồ Chơi Laser Cho Mèo",
                Description =
                    "Đồ chơi laser tương tác mang lại niềm vui vô tận cho mèo cưng của bạn.",
                BasePrice = 85000m,
                Weight = 0.1m,
                Length = 12.0m,
                Height = 3.0m,
                Sold = 445,
                StarAverage = 4.3,
                ReviewCount = 123,
                TotalRating = 529,
                CreationDate = DateTime.UtcNow,
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = catToysCategory.Id,
                StoreId = stores[1].Id,
                Name = "Bộ Đồ Chơi Chuột Có Cỏ Mèo",
                Description =
                    "Bộ 3 chuột đồ chơi có tẩm cỏ mèo. Hoàn hảo để khuyến khích bản năng săn mồi tự nhiên.",
                BasePrice = 65000m,
                Weight = 0.2m,
                Length = 8.0m,
                Height = 5.0m,
                Sold = 278,
                StarAverage = 4.6,
                ReviewCount = 78,
                TotalRating = 359,
                CreationDate = DateTime.UtcNow,
            },
            // Pet Shampoo
            //new Product
            //{
            //    Id = Guid.NewGuid(),
            //    CategoryId = shampooCategory.Id,
            //    StoreId = stores[2].Id,
            //    Name = "Sữa Tắm Thú Cưng Dịu Nhẹ - Yến Mạch",
            //    Description =
            //        "Sữa tắm dị ứng với yến mạch cho da nhạy cảm. Làm cho lông mềm mượt và bóng mượt.",
            //    BasePrice = 140000m,
            //    Weight = 0.5m,
            //    Length = 18.0m,
            //    Height = 6.0m,
            //    Sold = 167,
            //    StarAverage = 4.8,
            //    ReviewCount = 45,
            //    TotalRating = 216,
            //    CreationDate = DateTime.UtcNow,
            //},
            // Collars & Leashes
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = collarsCategory.Id,
                StoreId = stores[0].Id,
                Name = "Vòng Cổ Chó Điều Chỉnh - Da Thật",
                Description =
                    "Vòng cổ chó da thật cao cấp với khóa điều chỉnh. Thoải mái và bền bỉ cho sử dụng hàng ngày.",
                BasePrice = 220000m,
                Weight = 0.2m,
                Length = 60.0m,
                Height = 3.0m,
                Sold = 134,
                StarAverage = 4.5,
                ReviewCount = 38,
                TotalRating = 171,
                CreationDate = DateTime.UtcNow,
            },
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();

        // Add product variants
        await SeedProductVariants(context, products);

        // Add product images
        await SeedProductImages(context, products);
    }

    private static async Task SeedProductVariants(
        PetDatabaseContext context,
        List<Product> products
    )
    {
        if (await context.ProductVariations.AnyAsync())
            return;

        var variants = new List<ProductVariant>();

        foreach (var product in products)
        {
            // Add size variants for most products
            var sizeVariants = new[]
            {
                new
                {
                    Size = "Nhỏ",
                    PriceModifier = 0.0m,
                    Stock = 50,
                },
                new
                {
                    Size = "Vừa",
                    PriceModifier = 50000m,
                    Stock = 75,
                },
                new
                {
                    Size = "Lớn",
                    PriceModifier = 100000m,
                    Stock = 30,
                },
            };

            foreach (var variant in sizeVariants)
            {
                var attributes = new { Size = variant.Size, Color = "Tiêu Chuẩn" };

                variants.Add(
                    new ProductVariant
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        Attributes = JsonDocument.Parse(JsonSerializer.Serialize(attributes)),
                        Price = product.BasePrice + variant.PriceModifier,
                        Stock = variant.Stock,
                        CreationDate = DateTime.UtcNow,
                    }
                );
            }
        }

        await context.ProductVariations.AddRangeAsync(variants);
        await context.SaveChangesAsync();
    }

    private static async Task SeedProductImages(PetDatabaseContext context, List<Product> products)
    {
        if (await context.ProductImages.AnyAsync())
            return;

        var images = new List<ProductImage>();

        foreach (var product in products)
        {
            // Add main image
            images.Add(
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    IsMain = true,
                    ImageUrl = $"https://example.com/products/{product.Id}/main.jpg",
                    CreationDate = DateTime.UtcNow,
                }
            );

            // Add additional images
            for (int i = 1; i <= 3; i++)
            {
                images.Add(
                    new ProductImage
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        IsMain = false,
                        ImageUrl = $"https://example.com/products/{product.Id}/image-{i}.jpg",
                        CreationDate = DateTime.UtcNow,
                    }
                );
            }
        }

        await context.ProductImages.AddRangeAsync(images);
        await context.SaveChangesAsync();
    }
}
