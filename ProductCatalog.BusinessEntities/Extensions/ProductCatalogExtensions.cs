using ProductCatalog.BusinessEntities.Models;

namespace ProductCatalog.BusinessEntities.Extensions
{
    public static class ProductCatalogExtensions
    {
        public static void Map(this ProductCatalogModel dbProductCatalog, ProductCatalogModel ProductCatalog)
        {
            dbProductCatalog.Name = ProductCatalog.Name;
            dbProductCatalog.Photo = ProductCatalog.Photo;
            dbProductCatalog.Price = ProductCatalog.Price;
            dbProductCatalog.LastUpated = ProductCatalog.LastUpated;
            dbProductCatalog.DateCreated = ProductCatalog.DateCreated;
        }
    }
}
