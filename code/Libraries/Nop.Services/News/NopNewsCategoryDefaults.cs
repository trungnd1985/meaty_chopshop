using Nop.Core.Caching;
using Nop.Core.Domain.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.News
{
    public class NopNewsCategoryDefaults
    {
        public static CacheKey CategoriesAllCacheKey => new CacheKey("Nop.newscategory.all.{0}-{1}-{2}", NopEntityCacheDefaults<NewsCategory>.AllPrefix);

        public static CacheKey CategoriesByParentCategoryCacheKey => new CacheKey("Nop.newscategory.byparent.{0}-{1}-{2}", CategoriesByParentCategoryPrefix);

        public static string CategoriesByParentCategoryPrefix => "Nop.newscategory.byparent.{0}";

        public static CacheKey CategoryBreadcrumbCacheKey => new CacheKey("Nop.newscategory.breadcrumb.{0}-{1}-{2}-{3}", CategoryBreadcrumbPrefix);

        public static string CategoryBreadcrumbPrefix => "Nop.newscategory.breadcrumb.";

        public static CacheKey CategoriesChildIdsCacheKey => new CacheKey("Nop.newscategory.childids.{0}-{1}-{2}-{3}", CategoriesChildIdsPrefix);

        public static string CategoriesChildIdsPrefix => "Nop.newscategory.childids.{0}";

        public static CacheKey NewsInCategoriesByNewsCacheKey => new CacheKey("Nop.newsincategory.bynews.{0}-{1}-{2}", NewsInCategoriesByNewsPrefix);

        public static string NewsInCategoriesByNewsPrefix => "Nop.newsincategory.bynews.{0}";

        public static CacheKey NewsInCategoriesByCategoryCacheKey => new CacheKey("Nop.newsincategory.bycategory.{0}-{1}-{2}", NewsInCategoriesByCategoryPrefix);

        public static string NewsInCategoriesByCategoryPrefix => "Nop.newsincategory.bycategory.{0}";

    }
}
