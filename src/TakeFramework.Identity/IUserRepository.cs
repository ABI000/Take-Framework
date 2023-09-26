using TakeFramework.Domain.Entities;
using TakeFramework.Domain.Repositories;

namespace TakeFramework.Identity
{
    public interface IUserRepository<TUser, TPrimaryKey> : IBaseRepository<TUser, TPrimaryKey>
        where TUser : class, IEntity<TPrimaryKey>
    {

    }
}
