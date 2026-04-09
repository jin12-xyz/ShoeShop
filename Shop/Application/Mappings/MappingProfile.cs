using AutoMapper;
using Shop.Application.DTOs.Auth;
using Shop.Application.DTOs.Category;
using Shop.Models.Domain;
using Shop.Web.ViewModels.Auth;
using Shop.Web.ViewModels.Category;

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

            // Domain ↔ Read DTO
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ProductCount,
                            opt => opt.MapFrom(src => src.Products.Count));
            // Create
            CreateMap<CreateCategoryDto, Category>();
            // Update
            CreateMap<UpdateCategoryDto, Category>();


            // ViewData -> DTO
            CreateMap<CategoryDto, CategoryViewModel>();
            CreateMap<CategoryFormViewModel, CreateCategoryDto>();
            CreateMap<CategoryFormViewModel, UpdateCategoryDto>()
                .ForMember(dest => dest.Id,
                            opt => opt.MapFrom(src => src.Id));

        }
    }
}
