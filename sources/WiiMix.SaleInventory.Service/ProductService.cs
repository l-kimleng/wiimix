using AutoMapper;
using System.Collections.Generic;
using WiiMix.Business.Model;
using WiiMix.Data;

namespace WiiMix.SaleInventory.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> Find()
        {
            using (_unitOfWork)
            {
                var productRepository = _unitOfWork.ProductRepository;
                foreach (var product in productRepository.Find())
                {
                    yield return (Mapper.Map<Product>(product));
                }
            }
        }

        public Product FindUpdate(int id)
        {
            using (_unitOfWork)
            {
                var product = _unitOfWork.ProductRepository.FindUpdate(id);
                return Mapper.Map<Product>(product);
            }
        }

        public int Update(Product product)
        {
            using (_unitOfWork)
            {
                var productSaved = _unitOfWork.ProductRepository.FindUpdate(product.Id);
                productSaved.Name = product.Name;
                productSaved.CategoryId = product.Category.Id;
                productSaved.BrandId = product.Brand.Id;

                productSaved.Config.Feature = product.Config.Feature;
                productSaved.Config.Price = product.Config.Price;
                productSaved.Config.Image = product.Config.Image;
                productSaved.Config.ProductId = product.Id;
                _unitOfWork.ProductRepository.Update(productSaved);
                return _unitOfWork.Completed();
            }
        }

        public Product Add(Product product)
        {
            using (_unitOfWork)
            {
                var productAdded = Mapper.Map<Data.Entities.Product>(product);
                productAdded.Category = null;
                productAdded.Brand = null;
                var p =_unitOfWork.ProductRepository.Add(productAdded);
                _unitOfWork.Completed();
                product.Id = p.Id;
                product.CategoryId = p.CategoryId;
                product.BrandId = p.BrandId;
                product.Config.ProductId = p.Id;
                return product;
            }
        }
    }
}
