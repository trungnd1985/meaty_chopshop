using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Catalog
{
    public interface IMeatyProductService
    {
        Task<IList<Product>> GetAllProductsDisplayedOnHomepageAsync(int categoryId);
    }
}
