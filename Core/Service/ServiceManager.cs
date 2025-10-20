using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper,IBasketReppsitory basketReppsitory) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>( () => new ProductService(_unitOfWork,_mapper));

        public IProductService ProductService => _LazyProductService.Value;

        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>( () => new BasketService(basketReppsitory,_mapper) );

        public IBasketService BasketService => _LazyBasketService.Value;
    }
}
