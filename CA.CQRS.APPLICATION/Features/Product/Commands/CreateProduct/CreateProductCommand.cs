using CA.CQRS.APPLICATION.Common.Wrappers;
using CA.CQRS.CONTRACTS.Product.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CQRS.APPLICATION.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ApiResponse<ProductResponse>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}