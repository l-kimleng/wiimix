using AutoMapper;
using WiiMix.Data.Entities;

namespace WiiMix.SaleInventory
{
    public class SaleInventoryMappingProfile : Profile
    {
        public SaleInventoryMappingProfile()
        {
            CreateMap<Product, Business.Model.Product>()
                .ForMember(d => d.Details, opt => opt.Ignore());
            CreateMap<Category, Business.Model.Category>()
                .ForMember(c => c.Products, opt => opt.Ignore());
            CreateMap<Brand, Business.Model.Brand>()
                .ForMember(b => b.Products, opt => opt.Ignore());
            CreateMap<Config, Business.Model.Config>()
                .ForMember(c => c.Product, opt => opt.Ignore());
            CreateMap<StockDetail, Business.Model.StockDetail>()
                .ForMember(c => c.Stock, opt => opt.Ignore());
            CreateMap<Stock, Business.Model.Stock>();

            CreateMap<Business.Model.Product, Product>()
                .ForMember(d => d.Details, opt => opt.Ignore());
            CreateMap<Business.Model.Category, Category>()
                .ForMember(c => c.Products, opt => opt.Ignore());
            CreateMap<Business.Model.Brand, Brand>()
                .ForMember(b => b.Products, opt => opt.Ignore());
            CreateMap<Business.Model.Config, Config>()
                .ForMember(c => c.Product, opt => opt.Ignore());
            CreateMap<Business.Model.StockDetail, StockDetail>()
                .ForMember(c => c.Stock, opt => opt.Ignore());
            CreateMap<Business.Model.Stock, Stock>();
        }
    }
}