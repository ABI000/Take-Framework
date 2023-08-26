using TakeFramework.Domain.Repositories;
using TakeFramework.Identity.PO;

namespace TakeFramework.Identity
{
    public interface IUserRepository: IBaseRepository<User, long>
    {
    }
}
