using AutoMapper;
using Data.Models;
using SWP391_BE.Dtos.Product;
using SWP391_BE.Dtos.SkinType;

namespace SWP391_BE.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName));
            
            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name));
            
            CreateMap<SkinType, SkinTypeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SkinTypeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SkinTypeName));
            
            CreateMap<SkinRoutine, SkinRoutineDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SkinRoutineId));
            
            CreateMap<RoutineStep, RoutineStepDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoutineStepId));
        }
    }
} 