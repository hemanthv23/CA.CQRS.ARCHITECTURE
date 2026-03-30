using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CQRS.DOMAIN.Common
{
    public abstract class BaseEntity
    {
        public bool IsActive { get; set; } = true;       // default true
        public bool IsDeleted { get; set; } = false;     // default false
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // default now
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
