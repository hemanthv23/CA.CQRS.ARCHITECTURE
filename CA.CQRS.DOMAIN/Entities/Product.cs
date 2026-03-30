using CA.CQRS.DOMAIN.Common;
using CA.CQRS.DOMAIN.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CQRS.DOMAIN.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Active;
    }
}
