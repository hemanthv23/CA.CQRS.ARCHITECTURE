using CA.CQRS.APPLICATION.Common.Wrappers;
using CA.CQRS.CONTRACTS.Product.Responses;
using CA.CQRS.DOMAIN.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CQRS.APPLICATION.Features.Product.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<ApiResponse<PagedResponse<ProductResponse>>>
    {
        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Global Search
        public string? SearchKeyword { get; set; }

        // Sorting
        public string? SortBy { get; set; } = "CreatedAt";
        public string? SortDirection { get; set; } = "desc";

        // Filters
        public ProductStatus? Status { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public bool? IsActive { get; set; }
    }
}
