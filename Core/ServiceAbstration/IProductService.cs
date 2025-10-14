using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceAbstration
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        Task<ProductDto?> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();


    }
}
