﻿
using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    class ProductWithBrandAndTypeSpecification : BaseSpeceification<Product, int>
    {
        public ProductWithBrandAndTypeSpecification(ProductQueryParams queryParams) :
            base(P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId)
            && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (queryParams.sortingOption) 
            { 
                case ProductSortingOption.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOption.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOption.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOption.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }

        public ProductWithBrandAndTypeSpecification(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
            

    }
}
