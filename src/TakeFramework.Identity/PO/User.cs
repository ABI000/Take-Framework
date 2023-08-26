using Microsoft.AspNetCore.Identity;
using TakeFramework.Domain.Entities;

namespace TakeFramework.Identity.PO
{
    public class User : IdentityUser<long>, IFullAuditEntity<long>, IEntity<long>
    {
        public DateTime? DeleteTime { get; set; }
        public long DeleteBy { get; set; }
        public DateTime Created { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public long ModifiedBy { get; set; }
        public bool Deleted { get; set; }
    }
}