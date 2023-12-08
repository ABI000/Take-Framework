using Microsoft.Extensions.Options;
using TakeFramework.EntityFrameworkCore;
using TakeFramework.Identity.PO;

namespace TakeFramework.Identity.Repositories
{
    public class UserRepository(IEnumerable<IDbContextProvider> dbContextProviders, IOptions<IdentitySettings> identitySettings, EntityDictionary entityDictionary) 
    : EFCoreRepository<User, long>(dbContextProviders, identitySettings, entityDictionary), IUserRepository<User, long>
    {
    }
}



