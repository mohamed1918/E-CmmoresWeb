using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    var productBrandsData = File.ReadAllText(@"..\Infrastructure\\Persistence\\Data\\DataSeed\\brands.json");
                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                    if (productBrands is not null && productBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(productBrands);
                    }

                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypesData = File.ReadAllText(@"..\Infrastructure\\Persistence\\Data\\DataSeed\\types.json");
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                    if (productTypes is not null && productTypes.Any())
                    {
                        _dbContext.ProductTypes.AddRange(productTypes);
                    }

                }

                if (!_dbContext.Products.Any())
                {
                    var productsData = File.ReadAllText(@"..\Infrastructure\\Persistence\\Data\\DataSeed\\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products is not null && products.Any())
                    {
                        _dbContext.Products.AddRange(products);
                    }

                }


                _dbContext.SaveChanges();
            }
            catch (Exception ex) 
            {

            }
            
        }
    }
}
