
namespace FurEverCarePlatform.Application.Features.ProductCategory.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
