using AutoMapper;
using System.Collections.Generic;
using WiiMix.Business.Model;
using WiiMix.Data;

namespace WiiMix.SaleInventory.Service
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Brand> GetAll()
        {
            using (_unitOfWork)
            {
                var brands = _unitOfWork.Brands.GetAll();
                foreach (var brand in brands)
                {
                    yield return Mapper.Map<Brand>(brand);
                }
            }
        }
    }
}