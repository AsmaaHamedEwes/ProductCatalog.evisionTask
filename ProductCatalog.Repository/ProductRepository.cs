using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.BusinessEntities;
using ProductCatalog.BusinessEntities.ExtendedModels;
using ProductCatalog.BusinessEntities.Extensions;
using ProductCatalog.BusinessEntities.Models;
using ProductCatalog.Contracts;

namespace ProductCatalog.Repository
{
    class ProductRepository : RepositoryBase<ProductCatalogModel>, IProductRepository
    {
        public ProductRepository(ProductRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<ProductCatalogModel>> GetAllProductsAsync()
        {
            return await GetAll()
                 .OrderBy(x => x.Name)
                 .ToListAsync();
        }

        public async Task<ProductCatalogModel> GetProductByIdAsync(Guid ProductId)
        {
            return await GetByCondition(o => o.Id.Equals(ProductId))
                  .DefaultIfEmpty(new ProductCatalogModel())
                  .SingleAsync();
        }
        public async Task CreateProductAsync(ProductCatalogModel Product)
        {
            Product.Id = Guid.NewGuid();
            Product.DateCreated = DateTime.UtcNow.AddHours(2);
            Product.LastUpated = DateTime.UtcNow.AddHours(2); 
            Create(Product);
            await SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductCatalogModel dbProduct, ProductCatalogModel Product)
        {

            dbProduct.Map(Product);
            dbProduct.LastUpated = DateTime.UtcNow.AddHours(2);
            Update(dbProduct);
            await SaveChangesAsync();
        }

        public async Task DeleteProductAsync(ProductCatalogModel Product)
        {
            Delete(Product);
            await SaveChangesAsync();
        }

        public string ExportProductExcel(IEnumerable<ProductCatalogModel> ProductsCatalog)
        {
            var ConvertedProductsTable = ConvertToDataTable<ProductCatalogModel>(ProductsCatalog.ToList());
            ExcelUtlity Excelobj = new ExcelUtlity();
            Excelobj.WriteDataTableToExcel(ConvertedProductsTable, "Products Catalog Details", "C:\\ProductsExceldata.xlsx"+DateTime.UtcNow, "Products Details");
            return "C:\\ProductsExceldata.xlsx"+DateTime.UtcNow;
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)

            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)

            {

                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)

                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);

            }

            return table;

        }
    }
}

