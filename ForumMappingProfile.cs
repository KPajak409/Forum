using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Forum.Entities;
using Forum.Models;

namespace Forum
{
    public class ForumMappingProfile : Profile
    {
        public ForumMappingProfile()
        {
            CreateMap<CreateOrUpdateCategoryDto, Category>();
            CreateMap<Category, CreateOrUpdateCategoryDto>();
            CreateMap<CreateTopicDto, Topic>();
            CreateMap<CreateResponseDto, Response>();
            CreateMap<BanUserDto, BlackList>();
            CreateMap<User, UserDto>();
        }
    }
}
