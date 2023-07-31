using Microsoft.EntityFrameworkCore;

namespace TakeFramework.EntityFrameworkCore
{
    public interface IDbContextProvider
    {
        public string Name { get; }
    }
}
