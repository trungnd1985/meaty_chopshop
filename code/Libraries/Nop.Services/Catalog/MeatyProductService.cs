using Nop.Core.Domain.Catalog;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Catalog
{
    public class MeatyProductService : IMeatyProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;

        public MeatyProductService(IRepository<Product> productRepository
            , IRepository<ProductCategory> productCategoryRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<IList<Product>> GetAllProductsDisplayedOnHomepageAsync(int categoryId)
        {
            var products = await _productRepository.GetAllAsync(query =>
            {
                return from p in query
                       join pc in _productCategoryRepository.Table on p.Id equals pc.ProductId
                       orderby p.DisplayOrder, p.Id
                       where p.Published &&
                             !p.Deleted &&
                             p.ShowOnHomepage &&
                             pc.CategoryId == categoryId
                       select p;
            }, cache => cache.PrepareKeyForDefaultCache(new Core.Caching.CacheKey($"{NopCatalogDefaults.ProductsHomepageCacheKey}_{categoryId}")));

            return products;
        }
    }
}
