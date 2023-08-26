using Microsoft.Extensions.Options;
using TakeFramework.EntityFrameworkCore;
using TakeFramework.Identity.PO;

namespace TakeFramework.Identity.Repositories
{
    public class UserRepository : EFCoreRepository<User, long>
    {
        public UserRepository(IEnumerable<IDbContextProvider> dbContextProviders, IOptions<IdentitySettings> identitySettings) : base(dbContextProviders, identitySettings)
        {
        }
    }
}



