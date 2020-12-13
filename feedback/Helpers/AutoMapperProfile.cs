using FeedBack.Dtos;
using FeedBack.Models;
using AutoMapper;

namespace FeedBack.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}