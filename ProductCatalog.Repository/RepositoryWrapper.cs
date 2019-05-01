using System;
using System.Collections.Generic;
using System.Text;
using ProductCatalog.BusinessEntities;
using ProductCatalog.Contracts;

namespace ProductCatalog.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ProductRepositoryContext _repoContext;
        private IProductRepository _Product;
   
        

        public IProductRepository Product
        {
            get
            {
                if (_Product == null)
                {
                    _Product = new ProductRepository(_repoContext);
                }

                return _Product;
            }
        }

        public RepositoryWrapper(ProductRepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
    }
}
