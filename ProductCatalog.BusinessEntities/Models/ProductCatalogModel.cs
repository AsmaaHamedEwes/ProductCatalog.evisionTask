using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.BusinessEntities.Models
{
    public class ProductCatalogModel : IEntity
    {
        [Key]
        [Column("ProductId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "Date of Last Upated is required")]
        public DateTime LastUpated { get; set; }
    }
}
