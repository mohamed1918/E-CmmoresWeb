using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstration;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAllAsync();
            var brandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandsDto;
        }


        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var ptoducts = await _unitOfWork.GetRepository<Product,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(ptoducts);
        }


        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductType,int>();
            var types = await repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeDto>>(types);
        }


        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var product = await repo.GetByIdAsync(id);
            if (product is null)
                return null;
            return _mapper.Map<Product , ProductDto>(product);
        }

       
    }
}
