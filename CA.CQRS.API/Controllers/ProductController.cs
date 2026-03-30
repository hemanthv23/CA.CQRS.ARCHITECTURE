using CA.CQRS.APPLICATION.Common.Wrappers;
using CA.CQRS.APPLICATION.Features.Product.Commands.CreateProduct;
using CA.CQRS.APPLICATION.Features.Product.Queries.GetAllProducts;
using CA.CQRS.CONTRACTS.Product.Requests;
using CA.CQRS.CONTRACTS.Product.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CA.CQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-product")]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> Create(
            [FromBody] CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }


        [HttpGet("get-all-products")]
        public async Task<ActionResult<ApiResponse<PagedResponse<ProductResponse>>>> GetAll(
            [FromQuery] GetAllProductsRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetAllProductsQuery
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchKeyword = request.SearchKeyword,
                SortBy = request.SortBy,
                SortDirection = request.SortDirection,
                Status = request.Status,
                MinPrice = request.MinPrice,
                MaxPrice = request.MaxPrice,
                MinQuantity = request.MinQuantity,
                MaxQuantity = request.MaxQuantity,
                IsActive = request.IsActive
            };

            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }
    }
}