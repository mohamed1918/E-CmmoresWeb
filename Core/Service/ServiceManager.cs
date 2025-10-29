using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstration;
using System;

namespace Service
{
    public class ServiceManager(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IBasketReppsitory basketReppsitory,
        UserManager<ApplicationUser> _userManager,
        IConfiguration _configuration
    ) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService =
            new(() => new ProductService(_unitOfWork, _mapper));
        public IProductService ProductService => _lazyProductService.Value;

        private readonly Lazy<IBasketService> _lazyBasketService =
            new(() => new BasketService(basketReppsitory, _mapper));
        public IBasketService BasketService => _lazyBasketService.Value;

        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService =
            new(() => new AuthenticationService(_userManager, _configuration, _mapper));
        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;

        private readonly Lazy<IOrderService> _lazyOrderService =
            new(() => new OrderService(_mapper, basketReppsitory, _unitOfWork));
        public IOrderService OrderService => _lazyOrderService.Value;
    }
}
