using AutoMapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WiiMix.Business.Model;
using WiiMix.Data;

namespace WiiMix.SaleInventory.Service
{
    public class StockService : IStockService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Stock> FindAllDetail()
        {
            using (_unitOfWork)
            {
                var stocks = _unitOfWork.Stocks.FindAllDetail();
                foreach (var stock in stocks)
                {
                    yield return BuildStock(stock);
                }
            }
        }

        public Stock Add(Stock stock)
        {
            using (_unitOfWork)
            {
                var stockAdded = Mapper.Map<Data.Entities.Stock>(stock);
                
                var stockDb = _unitOfWork.Stocks.Add(stockAdded);
                _unitOfWork.Completed();
                return Mapper.Map<Stock>(stockDb);
            }
        }

        public int Update(Stock stock)
        {
            using (_unitOfWork)
            {
                var stockUpdated = _unitOfWork.Stocks.FindUpdate(stock.Id);
                stockUpdated.Date = stock.Date;
                stockUpdated.Quantity = stock.Quantity;
                stockUpdated.TotalPrice = stock.TotalPrice;
                foreach (var detail in stockUpdated.Details)
                {
                    var s = stock.Details.FirstOrDefault(d => d.ProductId == detail.ProductId);
                    if (s == null) continue;
                    detail.Quantity = s.Quantity;
                    detail.Price = s.Price;
                }
                _unitOfWork.Stocks.Update(stockUpdated);
                return _unitOfWork.Completed();
            }
        }

        private Stock BuildStock(Data.Entities.Stock stock)
        {
            var myStock = new Stock
            {
                Id = stock.Id,
                Date = stock.Date,
                Quantity = stock.Quantity,
                TotalPrice = stock.TotalPrice,
                Details = new ObservableCollection<StockDetail>()
            };
            foreach (var detail in stock.Details)
            {
                myStock.Details.Add(BuildStockDetail(detail));
            }

            return myStock;
        }

        private IList<Product> _products;
        private StockDetail BuildStockDetail(Data.Entities.StockDetail detail)
        {
            var myDetail = new StockDetail();
            using (_unitOfWork)
            {
                if (_products == null)
                {
                    _products = new List<Product>();
                    foreach (var product in _unitOfWork.Products.Find())
                    {
                        _products.Add(Mapper.Map<Product>(product));
                    }
                }
                var p = _products.SingleOrDefault(x => x.Id == detail.ProductId);
                if (p != null)
                {
                    myDetail.Id = detail.Id;
                    myDetail.ProductId = p.Id;
                    myDetail.Quantity = detail.Quantity;
                    myDetail.Price = detail.Price;
                    myDetail.StockId = detail.StockId;
                    myDetail.Product = p;

                }
            }
            return myDetail;
        }
    }
}