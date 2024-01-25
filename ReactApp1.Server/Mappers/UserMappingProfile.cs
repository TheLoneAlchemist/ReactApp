using AutoMapper;
using ReactApp1.Server.Models.Common;
using ReactApp1.Server.Models.DTOs;

namespace ReactApp1.Server.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterDTO, ApplicationUser>();
        }
    }
}
