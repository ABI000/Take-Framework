using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TakeFramework.Domain.Repositories;
using TakeFramework.EntityFrameworkCore;
using TakeFramework.Identity.PO;

namespace TakeFramework.Identity.Repositories
{
    public class RoleRepository : EFCoreRepository<Role, long>
    {
        public RoleRepository(IEnumerable<IDbContextProvider> dbContextProviders, IOptions<IdentitySettings> identitySettings) : base(dbContextProviders, identitySettings)
        {
        }
    }

}
