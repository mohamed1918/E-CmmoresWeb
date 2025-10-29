using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Service.Specifications;
using ServiceAbstration;
using Shared.DTO.IdentityModule;
using Shared.DTO.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class OrderService(IMapper _mapper, IBasketReppsitory _basketReppsitory,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            var address= _mapper.Map<AddressDto, OrderAddress>(orderDto.Address);

            var basket= await _basketReppsitory.GetBasketAsync(orderDto.BasketId);

            if (basket == null)
                throw new BasketNotFoundExeption(orderDto.BasketId);

            List<OrderItem> orderitems = new List<OrderItem>();
            var productRepo = _unitOfWork.GetRepository<Product,int>();

            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetByIdAsync(item.Id);
                if (product == null)
                    throw new ProductNotFoundException(item.Id);
                var orderItem = new OrderItem
                {
                    Product = new ProductItemOrdered
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    },
                    Price = product.Price,
                    Quantity = item.Quantity
                };
                orderitems.Add(orderItem);
            }

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetByIdAsync(orderDto.DeliveryMethodId);
            if (deliveryMethod is null)
                throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);


            var subTotal = orderitems.Sum(OI => OI.Price * OI.Quantity);

            var order = new Order(email, address, deliveryMethod, orderitems, subTotal);

            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order, OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrderByAsync(string email)
        {
            var spec = new OrderSpecification(email);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecification(id);
            var order = await  _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec);
            return _mapper.Map<Order, OrderToReturnDto>(order);
        }
    }
}
