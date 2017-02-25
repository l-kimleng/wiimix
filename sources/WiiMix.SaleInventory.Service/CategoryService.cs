using AutoMapper;
using System.Collections.Generic;
using WiiMix.Business.Model;
using WiiMix.Data;

namespace WiiMix.SaleInventory.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Category> GetAll()
        {
            using (_unitOfWork)
            {
                var categories = _unitOfWork.Categories.GetAll();
                foreach (var category in categories)
                {
                    yield return Mapper.Map<Category>(category);
                }
            }
        }
    }
}