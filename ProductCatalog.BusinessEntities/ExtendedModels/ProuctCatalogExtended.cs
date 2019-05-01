using System;
using ProductCatalog.BusinessEntities.Models;

namespace ProductCatalog.BusinessEntities.ExtendedModels
{
    public class ProuctCatalogExtended : IEntity
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public DateTime DateCreated { get; set; }
       
        public DateTime LastUpated { get; set; }
        public ProuctCatalogExtended()
        {

        }
        public ProuctCatalogExtended( ProductCatalogModel product )
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Photo = product.Photo;
            DateCreated = product.DateCreated;
            LastUpated = product.LastUpated; 
        }
    }
}
