using TakeFramework.EntityFrameworkCore;

namespace SampleWeb
{
    public class UserRepository : EFCoreRepository<User, long, SampleDbContext>
    {
        public UserRepository(IDbContextProvider<SampleDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}