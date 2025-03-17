using FurEverCarePlatform.Application.Features.ServiceCategory.Queries.GetServiceCategories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/v1/service-categories")]
    [ApiController]
    public class ServiceCategoryController : ControllerBase
    {
		private readonly IMediator _mediator;
		public ServiceCategoryController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
	    [ProducesResponseType(200)]
	    [ProducesResponseType(400)]
	    [ProducesResponseType(500)]
	    public async Task<IActionResult> GetAllPetServices()
	    {
		    var result = await _mediator.Send(new GetServiceCategoriesQuery { });
		    return Ok(result);
	    }
	}
}
