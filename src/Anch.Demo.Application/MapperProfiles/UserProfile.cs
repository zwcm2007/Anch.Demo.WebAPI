using AutoMapper;
using Anch.Demo.Core;

namespace Anch.Demo.Application
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User,UserDto>()
                .ForMember(dest => dest.UserCode, opt => opt.MapFrom(src => src.Id));

            //CreateMap<H, H_LS>()
            //    .ForMember(dest => dest.GXSJ, opt => opt.Ignore());
        }
    }
}