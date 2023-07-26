using Sample.Core;
using Sample.EntityFrameworkCore;
using TakeFramework.EntityFrameworkCore;

namespace Sample.Domain
{
    public class UserRepository : EFCoreRepository<User, long, SampleDbContext>
    {
        public UserRepository(IEnumerable<IDbContextProvider> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}