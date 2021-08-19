using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Factories
{
    public interface IMeatyCatalogModelFactory
    {
        Task<List<CategoryModel>> PrepareHomepageCategoryModelsAsync();
    }
}
