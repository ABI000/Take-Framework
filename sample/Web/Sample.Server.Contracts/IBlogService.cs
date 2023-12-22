using TakeFramework.Domain.Services;

namespace Sample.Server.Contracts;

public interface IBlogService : IBaseService
{
    public Task<BlogDto> CreateAsync(BlogDto dto);
    public Task SendEvent(BlogDto dto);

}
