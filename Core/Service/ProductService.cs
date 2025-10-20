using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModule;
using Service.Specifications;
using ServiceAbstration;
using Shared;
using Shared.DTO.ProductModule;
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


        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecification(queryParams);
            var ptoducts = await repo.GetAllAsync();
            var productsDto = _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(ptoducts);
            var productCount = productsDto.Count();
            var CountSpec = new ProductCountSpecification(queryParams);
            var totalCount = await repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDto>(queryParams.PageSize , queryParams.PageIndex , 0 ,  productsDto);
        }


        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductType,int>();
            var types = await repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeDto>>(types);
        }


        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecification(id);
            var product = await repo.GetByIdAsync(id);
            if (product is null)
                throw new ProductNotFoundException(id);
            return _mapper.Map<Product , ProductDto>(product);
        }

       
    }
}
