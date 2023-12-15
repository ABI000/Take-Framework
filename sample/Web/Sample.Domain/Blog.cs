using TakeFramework.Domain.Entities;

namespace Sample.Domain;

public class Blog : FullAuditEntity<long, long>
{
    public required string Title { get; set; }

}
