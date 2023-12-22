using AutoMapper;
using Sample.Domain;
using Sample.Server.Contracts;
using TakeFramework.Domain.Managers;
using TakeFramework.Domain.Services;
using TakeFramework.Domain.Uow;
using TakeFramework.EventBus;

namespace Sample.Server;
public class BlogService(IUnitOfWork unitOfWork, IBlogRepository blogRepository, IMapper mapper, IEventBus eventBus) : BaseService(unitOfWork), IBlogService, IBaseManager
{
    private readonly IBlogRepository _blogRepository = blogRepository;
    private readonly IMapper _mapper = mapper;

    private readonly IEventBus _eventBus = eventBus;
    [UnitOfWork]
    public async Task<BlogDto> CreateAsync(BlogDto dto)
    {
        var data = _mapper.Map<BlogDto, Blog>(dto);
        data.InIit(0);
        return _mapper.Map(await _blogRepository.CreateAsync(data), dto);
    }

    public Task SendEvent(BlogDto dto)
    {
        IntegrationEvent integrationEvent = new BlogIntegrationEvent(dto.Title);
        _eventBus.Publish(integrationEvent);
        return Task.CompletedTask;
    }
}
