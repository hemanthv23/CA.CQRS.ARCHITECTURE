using CA.CQRS.APPLICATION.Common.Wrappers;
using CA.CQRS.APPLICATION.Interfaces;
using CA.CQRS.CONTRACTS.Product.Responses;
using CA.CQRS.DOMAIN.Enums;
using CA.CQRS.DOMAIN.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CQRS.APPLICATION.Features.Product.Commands.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new DOMAIN.Entities.Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Quantity = request.Quantity,
                Status = ProductStatus.Active
            };

            var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);

            var response = new ProductResponse
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                Description = createdProduct.Description,
                Price = createdProduct.Price,
                Quantity = createdProduct.Quantity,
                Status = createdProduct.Status,
                CreatedAt = createdProduct.CreatedAt,
                CreatedBy = createdProduct.CreatedBy
            };

            return ApiResponse<ProductResponse>.Success(response, "Product created successfully");
        }
    }
}
