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
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>();
            
            CreateMap<SkinType, SkinTypeDto>();
            CreateMap<SkinRoutine, SkinRoutineDto>();
            CreateMap<RoutineStep, RoutineStepDto>();
        }
    }
} 