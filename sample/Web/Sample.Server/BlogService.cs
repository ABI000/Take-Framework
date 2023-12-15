using AutoMapper;
using Sample.Domain;
using Sample.Server.Contracts;
using TakeFramework.Domain.Managers;
using TakeFramework.Domain.Services;
using TakeFramework.Domain.Uow;

namespace Sample.Server;
public class BlogService(IUnitOfWork unitOfWork, IBlogRepository blogRepository, IMapper mapper) : BaseService(unitOfWork), IBlogService, IBaseManager
{
    private readonly IBlogRepository _blogRepository = blogRepository;
    private readonly IMapper _mapper = mapper;
    [UnitOfWork]
    public async Task<BlogDto> CreateAsync(BlogDto dto)
    {
        var data = _mapper.Map<BlogDto, Blog>(dto);
        data.InIit(0);
        return _mapper.Map(await _blogRepository.CreateAsync(data), dto);
    }
}
