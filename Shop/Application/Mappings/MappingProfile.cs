using AutoMapper;
using Shop.Application.DTOs.Auth;
using Shop.Models.Domain;
using Shop.Web.ViewModels.Auth;

namespace Shop.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName,
                            opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email,
                            opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Fullname,
                            opt => opt.MapFrom(src => src.Fullname))
                .ForMember(dest => dest.PhoneNumber,
                            opt => opt.MapFrom(src => src.PhoneNumber));

            // RegisterViewModel → RegisterDto
            CreateMap<RegisterViewModel, RegisterDto>();

            // LoginViewModel → LoginDto
            CreateMap<LoginViewModel, LoginDto>();
        }
    }
}
