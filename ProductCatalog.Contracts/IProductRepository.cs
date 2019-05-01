using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.BusinessEntities.ExtendedModels;
using ProductCatalog.BusinessEntities.Models;

namespace ProductCatalog.Contracts
{
   public  interface IProductRepository:IRepositoryBase<ProductCatalogModel>
    {
        Task<IEnumerable<ProductCatalogModel>> GetAllProductsAsync();
        Task<ProductCatalogModel> GetProductByIdAsync(Guid ProductId);
        Task CreateProductAsync(ProductCatalogModel Product);
        Task UpdateProductAsync(ProductCatalogModel dbProduct, ProductCatalogModel Product);
        Task DeleteProductAsync(ProductCatalogModel Product);
        string ExportProductExcel(IEnumerable<ProductCatalogModel> productCatalog);
    }
}
