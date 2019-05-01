using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.BusinessEntities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
