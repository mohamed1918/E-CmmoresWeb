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
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>( () => new ProductService(_unitOfWork,_mapper));

        public IProductService ProductService => _LazyProductService.Value;
    }
}
