using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPLite.Domain.Common;


namespace ERPLite.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedOnUtc { get; set; }
        = DateTime.UtcNow;

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOnUtc { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}

