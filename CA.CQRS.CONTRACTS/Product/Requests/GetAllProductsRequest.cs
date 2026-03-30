using CA.CQRS.DOMAIN.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CQRS.CONTRACTS.Product.Requests
{
    public class GetAllProductsRequest
    {
        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Global Search
        public string? SearchKeyword { get; set; }

        // Sorting
        public string? SortBy { get; set; } = "CreatedAt";
        public string? SortDirection { get; set; } = "asc";

        // Filters
        public ProductStatus? Status { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public bool? IsActive { get; set; }
    }
}
