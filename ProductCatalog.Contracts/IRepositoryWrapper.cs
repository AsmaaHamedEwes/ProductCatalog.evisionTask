using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Contracts
{
    public interface IRepositoryWrapper
    {
        IProductRepository Product { get; }
    }
}
