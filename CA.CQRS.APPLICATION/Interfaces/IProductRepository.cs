using CA.CQRS.DOMAIN.Entities;
using CA.CQRS.DOMAIN.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CQRS.APPLICATION.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken);
        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<(IReadOnlyList<Product> Items, int TotalCount, int PageNumber, int PageSize)> GetAllAsync(
              int pageNumber,
              int pageSize,
              string? searchKeyword,
              string? sortBy,
              string? sortDirection,
              ProductStatus? status,
              decimal? minPrice,
              decimal? maxPrice,
              int? minQuantity,
              int? maxQuantity,
              bool? isActive,
              CancellationToken cancellationToken);
        Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    }
}
