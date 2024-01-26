using Sample.Domain;
using TakeFramework;
using TakeFramework.EntityFrameworkCore;

namespace Sample.EntityFrameworkCore;

public class BlogRepository(IEnumerable<IDbContextProvider> dbContextProviders, EntityDictionary entityDictionary) :
EFCoreRepository<Blog, long, SampleDbContext>(dbContextProviders, entityDictionary), IBlogRepository
{
}
