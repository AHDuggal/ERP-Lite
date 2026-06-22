using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedOnUtc { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime? ModifiedOnUtc { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
