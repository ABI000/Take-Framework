using Microsoft.AspNetCore.Identity;
using TakeFramework.Domain.Entities;

namespace TakeFramework.Identity.PO
{
    public class RoleClaim : IdentityRoleClaim<long>, IFullAuditEntity<long>, IEntity<long>
    {
        public new long Id { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long DeleteBy { get; set; }
        public DateTime Created { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public long ModifiedBy { get; set; }
        public bool Deleted { get; set; }
    }
}