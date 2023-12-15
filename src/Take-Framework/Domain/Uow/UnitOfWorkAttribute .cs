

namespace TakeFramework.Domain.Uow
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute : Attribute
    {
        public string? DBName { get; set; }

    }
}
