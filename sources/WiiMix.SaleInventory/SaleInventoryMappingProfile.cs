using AutoMapper;
using WiiMix.Data.Entities;

namespace WiiMix.SaleInventory
{
    public class SaleInventoryMappingProfile : Profile
    {
        public SaleInventoryMappingProfile()
        {
            CreateMap<Product, Models.Product>()
                .ForMember(d => d.Details, opt => opt.Ignore());
            CreateMap<Category, Models.Category>()
                .ForMember(c => c.Products, opt => opt.Ignore());
            CreateMap<Brand, Models.Brand>()
                .ForMember(b => b.Products, opt => opt.Ignore());
            CreateMap<Config, Models.Config>()
                .ForMember(c => c.Product, opt => opt.Ignore());
        }
    }
}