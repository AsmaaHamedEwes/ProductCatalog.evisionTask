using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.BusinessEntities.Extensions;
using ProductCatalog.BusinessEntities.Models;
using ProductCatalog.Contracts;

namespace ProductCatalog.Services.Controllers
{
    /// <summary>
    /// Product Catalog Manager Controller
    /// Route("api/Product")
    /// </summary>
    [Route("api/Product")]
    [ApiController]
     [ApiExplorerSettings(IgnoreApi = false)]
    public class ProductCatalogManagerController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        /// <summary>
        /// Product Catalog Manager ctor 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public ProductCatalogManagerController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns> IEnumerable : ProductCatalogModel </returns>
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var productsCatalog = await _repository.Product.GetAllProductsAsync();
                return Ok(productsCatalog);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllProducts action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Get Product ById
        /// </summary>
        /// <param name="id"></param>
        /// <returns> ProductCatalogModel </returns>
        [HttpGet("{id}", Name = "ProuctById")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _repository.Product.GetProductByIdAsync(id);

                if (product.IsEmptyObject())
                {
                    _logger.LogError($"product with id: {id}, hasn't been found in ProuctCatalogDB.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Product with id: {id}");
                    return Ok(product);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetProductById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        
        /// <summary>
        /// Create Product 
        /// </summary>
        /// <param name="Product"></param>
        
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody]ProductCatalogModel Product)
        {
            try
            {
                if (Product.IsObjectNull())
                {
                    _logger.LogError("Product object sent from client is null.");
                    return BadRequest("Product object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Product object sent from client.");
                    return BadRequest("Invalid Product model object");
                }

                await _repository.Product.CreateProductAsync(Product);

                return CreatedAtRoute("ProuctById", new { id = Product.Id }, Product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
      

        // PUT api/values/5
        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody]ProductCatalogModel product)
        {
            try
            {
                if (product.IsObjectNull())
                {
                    _logger.LogError("product object sent from client is null.");
                    return BadRequest("product object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid product object sent from client.");
                    return BadRequest("Invalid product model object");
                }

                var dbProduct = await _repository.Product.GetProductByIdAsync(id);
                if (dbProduct.IsEmptyObject())
                {
                    _logger.LogError($"Product with id: {id}, hasn't been found in ProuctCatalogDB.");
                    return NotFound();
                }

                await _repository.Product.UpdateProductAsync(dbProduct, product);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Delete Product {id}
        /// </summary>
        /// <param name="id"></param>
        
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var Product = await _repository.Product.GetProductByIdAsync(id);
                if (Product.IsEmptyObject())
                {
                    _logger.LogError($"Product with id: {id}, hasn't been found in ProuctCatalogDB.");
                    return NotFound();
                }

                await _repository.Product.DeleteProductAsync(Product);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Export Products Excel
        /// </summary>
        /// <param name="ProductsList"></param>
        /// <returns> Export  Path</returns>
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult ExportProductsExcel([FromBody] List<ProductCatalogModel> ProductsList)
        {
            try
            {
                if (ProductsList.Count == default(int))
                {
                    _logger.LogError("Products List sent from client is null.");
                    return BadRequest("Products List is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Products List sent from client.");
                    return BadRequest("Invalid Products List object");
                }

                var Path = _repository.Product.ExportProductExcel(ProductsList);
                return Ok(Path);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside ExportProductsExcel action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
