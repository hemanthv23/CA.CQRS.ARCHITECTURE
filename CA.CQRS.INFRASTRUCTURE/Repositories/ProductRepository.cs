using CA.CQRS.APPLICATION.Interfaces;
using CA.CQRS.DOMAIN.Entities;
using CA.CQRS.DOMAIN.Enums;
using CA.CQRS.INFRASTRUCTURE.DbContext;
using Microsoft.EntityFrameworkCore;

namespace CA.CQRS.INFRASTRUCTURE.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
        public async Task<(IReadOnlyList<Product> Items, int TotalCount, int PageNumber, int PageSize)> GetAllAsync(
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
            CancellationToken cancellationToken)
        {
            var query = _context.Products
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            // Global Search
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                query = query.Where(x =>
                    x.Name.Contains(searchKeyword) ||
                    x.Description.Contains(searchKeyword));
            }

            // Filters
            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (minPrice.HasValue)
                query = query.Where(x => x.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(x => x.Price <= maxPrice.Value);

            if (minQuantity.HasValue)
                query = query.Where(x => x.Quantity >= minQuantity.Value);

            if (maxQuantity.HasValue)
                query = query.Where(x => x.Quantity <= maxQuantity.Value);

            if (isActive.HasValue)
                query = query.Where(x => x.IsActive == isActive.Value);

            // Total Count before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Sorting
            query = sortBy?.ToLower() switch
            {
                "name" => sortDirection == "asc"
                    ? query.OrderBy(x => x.Name)
                    : query.OrderByDescending(x => x.Name),
                "price" => sortDirection == "asc"
                    ? query.OrderBy(x => x.Price)
                    : query.OrderByDescending(x => x.Price),
                "quantity" => sortDirection == "asc"
                    ? query.OrderBy(x => x.Quantity)
                    : query.OrderByDescending(x => x.Quantity),
                _ => sortDirection == "asc"
                    ? query.OrderBy(x => x.CreatedAt)
                    : query.OrderByDescending(x => x.CreatedAt)
            };

            // Pagination
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount, pageNumber, pageSize);
        }
        public Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}