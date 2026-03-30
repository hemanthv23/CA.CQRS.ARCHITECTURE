using CA.CQRS.APPLICATION.Common.Wrappers;
using CA.CQRS.APPLICATION.Interfaces;
using CA.CQRS.CONTRACTS.Product.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CQRS.APPLICATION.Features.Product.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, ApiResponse<PagedResponse<ProductResponse>>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<PagedResponse<ProductResponse>>> Handle(
            GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetAllAsync(
                request.PageNumber,
                request.PageSize,
                request.SearchKeyword,
                request.SortBy,
                request.SortDirection,
                request.Status,
                request.MinPrice,
                request.MaxPrice,
                request.MinQuantity,
                request.MaxQuantity,
                request.IsActive,
                cancellationToken);

            var response = new PagedResponse<ProductResponse>
            {
                Items = result.Items.Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Status = p.Status,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy
                }).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };

            return ApiResponse<PagedResponse<ProductResponse>>.Success(
                response, "Products retrieved successfully");
        }
    }
}
