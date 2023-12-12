using TakeFramework.Domain.Entities;

namespace TakeFramework.Identity.PO.Extension
{
    public class RoleCategory : FullAuditEntity<long, long>
    {
        public string Name { get; set; }
    }
}

