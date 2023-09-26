using TakeFramework.Domain.Entities;

namespace Sample.Core
{
    public class User : FullAuditEntity<long, long>
    {

        public string Name { get; set; } = string.Empty;
    }
}