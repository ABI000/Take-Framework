﻿using AutoMapper;
using Sample.Core;
using Sample.Domain;
using Sample.Domain.Shared;
using Sample.Server.Contracts;
using TakeFramework.Domain.Services;
using TakeFramework.Exceptions;

namespace Sample.Server
{
    public class TestService : BaseService
    {
        protected readonly UserRepository rpository;

        protected readonly IMapper mapper;
        public TestService(UserRepository rpository, IMapper mapper)
        {
            this.mapper = mapper;
            this.rpository = rpository;
        }
        public List<UserDto> List()
        {
            return mapper.Map<List<User>, List<UserDto>>(rpository.List());
        }

        public void GetException()
        {
            throw new BusinessException(ErrorCodes.ServerError, ErrorCodes.ServerErrorCode);
        }
    }
}