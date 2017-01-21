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
        }
    }
}