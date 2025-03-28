using FurEverCarePlatform.Application.Contracts;
using FurEverCarePlatform.Application.Exception;
using FurEverCarePlatform.Application.Features.Product.Commands.CreateProduct;
using FurEverCarePlatform.Application.Features.Product.DTOs;
using FurEverCarePlatform.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FurEverCarePlatform.Application.UnitTests.Features.Commands;
public class CreateProductHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<IGenericRepository<ProductPrice>> _mockProductPriceRepository;
    private readonly CreateProductHandler _handler;

    public CreateProductHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockProductRepository = new Mock<IProductRepository>();
        _mockProductPriceRepository = new Mock<IGenericRepository<ProductPrice>>();

        // Setup repository mocks
        _mockUnitOfWork
            .Setup(x => x.GetRepository<Product>())
            .Returns(_mockProductRepository.Object);
        _mockUnitOfWork
            .Setup(x => x.GetRepository<ProductPrice>())
            .Returns(_mockProductPriceRepository.Object);

        _handler = new CreateProductHandler(_mockUnitOfWork.Object);
    }

    private CreateProductCommand CreateValidProductCommand()
    {
        return new CreateProductCommand
        {
            Name = "Test Product",
            ProductCategoryId = Guid.NewGuid(),
            BrandId = Guid.NewGuid(),
            StoreId = Guid.NewGuid(),
            IsActive = true,
            ProductDescription = "Test Description",
            Weight = 10.5m,
            Length = 100,
            Height = 50,
            Width = 75,
            ProductTypes = new List<ProductTypeDTO>
            {
                new ProductTypeDTO
                {
                    Name = "Type 1",
                    ProductTypeDetails = new List<ProductTypeDetailsDTO>
                    {
                        new ProductTypeDetailsDTO { Name = "Detail 1" },
                        new ProductTypeDetailsDTO { Name = "Detail 2" }
                    }
                }
            },
            ProductImages = new List<ProductImagesDTO>
            {
                new ProductImagesDTO { URL = "http://test.com/image1.jpg" }
            },
            ProductPrices = new List<ProductPricesDTO>
            {
                new ProductPricesDTO
                {
                    ProductTypeDetails1 = "Detail 1",
                    ProductTypeDetails2 = "Detail 2",
                    Price = 100,
                    Inventory = 50
                }
            }
        };
    }

    [Fact]
    public async Task Handle_ValidProduct_ShouldCreateProductSuccessfully()
    {
        // Arrange
        var command = CreateValidProductCommand();

        // Setup mocks to allow product and price creation
        _mockProductRepository
            .Setup(x => x.InsertAsync(It.IsAny<Product>()))
            .Callback<Product>(p => p.Id = Guid.NewGuid())
            ;

        _mockProductPriceRepository
            .Setup(x => x.AddRangeAsync(It.IsAny<IEnumerable<ProductPrice>>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        // Verify transaction and repository methods
        _mockUnitOfWork.Verify(x => x.BeginTransactionAsync(), Times.Once);
        _mockUnitOfWork.Verify(x => x.CommitTransactionAsync(), Times.Once);

        _mockProductRepository.Verify(
            x => x.InsertAsync(It.Is<Product>(p =>
                p.Name == command.Name &&
                p.ProductTypes.Count == 1
            )),
            Times.Once
        );

        _mockProductPriceRepository.Verify(
            x => x.AddRangeAsync(It.Is<IEnumerable<ProductPrice>>(prices =>
                prices.Count() == 1 &&
                prices.First().Price == 100
            )),
            Times.Once
        );

        // Verify the returned ID is not empty
        Assert.NotEqual(Guid.Empty, result);
    }

    [Fact]
    public async Task Handle_InvalidProduct_ShouldThrowValidationException()
    {
        // Arrange
        var command = CreateValidProductCommand();
        command.Name = ""; // Invalid name to trigger validation

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() =>
            _handler.Handle(command, CancellationToken.None)
        );
    }


    [Fact]
    public async Task Handle_MissingProductTypeDetail_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = CreateValidProductCommand();
        // Modify to use a non-existent product type detail
        command.ProductPrices[0].ProductTypeDetails1 = "Non-Existent Detail";

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BadRequestException>(() =>
            _handler.Handle(command, CancellationToken.None)
        );

        Assert.Contains("Create product failed", exception.Message);
    }

    [Fact]
    public async Task Handle_ProductWithMultipleTypes_ShouldCreateSuccessfully()
    {
        // Arrange
        var command = CreateValidProductCommand();
        command.ProductTypes.Add(new ProductTypeDTO
        {
            Name = "Type 2",
            ProductTypeDetails = new List<ProductTypeDetailsDTO>
            {
                new ProductTypeDetailsDTO { Name = "Detail 3" }
            }
        });

        // Setup mocks
        _mockProductRepository
            .Setup(x => x.InsertAsync(It.IsAny<Product>()))
            .Callback<Product>(p => p.Id = Guid.NewGuid())
            ;

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockProductRepository.Verify(
            x => x.InsertAsync(It.Is<Product>(p =>
                p.ProductTypes.Count == 2
            )),
            Times.Once
        );

        Assert.NotEqual(Guid.Empty, result);
    }
}