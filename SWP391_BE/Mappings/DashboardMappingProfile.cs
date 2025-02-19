using AutoMapper;
using Data.Models;
using SWP391_BE.DTOs;

namespace SWP391_BE.Mappings
{
    public class DashboardMappingProfile : Profile
    {
        public DashboardMappingProfile()
        {
            CreateMap<DashboardReport, DashboardStatisticsDTO>()
                .ForMember(dest => dest.TotalSales, opt => opt.MapFrom(src => src.TotalSales))
                .ForMember(dest => dest.SalesGrowth, opt => opt.MapFrom(src => src.SalesGrowthRate))
                .ForMember(dest => dest.TotalOrders, opt => opt.MapFrom(src => src.TotalOrders))
                .ForMember(dest => dest.OrdersGrowth, opt => opt.MapFrom(src => src.OrdersGrowthRate))
                .ForMember(dest => dest.ActiveUsers, opt => opt.MapFrom(src => src.ActiveUsers))
                .ForMember(dest => dest.UserGrowth, opt => opt.MapFrom(src => src.UserGrowthRate))
                .ForMember(dest => dest.OverallGrowth, opt => opt.MapFrom(src => src.OverallGrowthRate))
                .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.LastUpdated))
                .ForMember(dest => dest.TimeRange, opt => opt.MapFrom(src => src.TimeRange));
        }
    }
} 