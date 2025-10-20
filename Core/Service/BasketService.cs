using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstration;
using Shared.DTO.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class BasketService(IBasketReppsitory _basketReppsitory, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var basketModel = _mapper.Map<BasketDto,Basket>(basket);
            var CreatedOrUpdatedBasket = await _basketReppsitory.CreateOrUpdateBasketAsync(basketModel);
            if (CreatedOrUpdatedBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can't not update or create basket now, try again later");
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _basketReppsitory.DeleteBasketAsync(key);
        }

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var basket = await _basketReppsitory.GetBasketAsync(key);
            if (basket == null)
                throw new BasketNotFoundExeption(key);

            return _mapper.Map<Basket,BasketDto>(basket);
        }
    }
}
