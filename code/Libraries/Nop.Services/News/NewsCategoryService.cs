using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.News;
using Nop.Data;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Stores;

namespace Nop.Services.News
{
    public class NewsCategoryService : INewsCategoryService
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly IRepository<NewsCategory> _categoryRepository;
        private readonly IRepository<NewsItem> _newsRepository;
        private readonly IRepository<NewsInCategory> _newsInCategoryRepository;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        #endregion
        #region Ctor

        public NewsCategoryService(
            ICustomerService customerService,
            ILocalizationService localizationService,
            IRepository<NewsCategory> categoryRepository,
            IRepository<NewsItem> productRepository,
            IRepository<NewsInCategory> productCategoryRepository,
            IStaticCacheManager staticCacheManager,
            IStoreContext storeContext,
            IWorkContext workContext)
        {
            _localizationService = localizationService;
            _categoryRepository = categoryRepository;
            _newsRepository = productRepository;
            _newsInCategoryRepository = productCategoryRepository;
            _staticCacheManager = staticCacheManager;
            _storeContext = storeContext;
            _workContext = workContext;
            _customerService = customerService;
        }

        #endregion

        public async Task DeleteCategoriesAsync(IList<NewsCategory> categories)
        {
            if (categories == null)
                throw new ArgumentNullException(nameof(categories));

            foreach (var category in categories)
                await DeleteCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(NewsCategory category)
        {
            await _categoryRepository.DeleteAsync(category);

            //reset a "Parent category" property of all child subcategories
            var subcategories = await GetAllCategoriesByParentCategoryIdAsync(category.Id, true);
            foreach (var subcategory in subcategories)
            {
                subcategory.ParentCategoryId = 0;
                await UpdateCategoryAsync(subcategory);
            }
        }

        public async Task DeleteNewsInCategoryAsync(NewsInCategory newsInCategory)
        {
            await _newsInCategoryRepository.DeleteAsync(newsInCategory);
        }

        public NewsInCategory FindNewsInCategory(IList<NewsInCategory> source, int newsId, int categoryId)
        {
            foreach (var productCategory in source)
                if (productCategory.NewsId == newsId && productCategory.NewsCategoryId == categoryId)
                    return productCategory;

            return null;
        }

        public async Task<IList<NewsCategory>> GetAllCategoriesAsync(int storeId = 0, bool showHidden = false)
        {
            var key = _staticCacheManager.PrepareKeyForDefaultCache(NopNewsCategoryDefaults.CategoriesAllCacheKey,
                storeId,
                await _customerService.GetCustomerRoleIdsAsync(await _workContext.GetCurrentCustomerAsync()),
                showHidden);

            var categories = await _staticCacheManager
                .GetAsync(key, async () => (await GetAllCategoriesAsync(string.Empty, storeId, showHidden: showHidden)).ToList());

            return categories;
        }

        public async Task<IPagedList<NewsCategory>> GetAllCategoriesAsync(string categoryName, int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, bool? overridePublished = null)
        {
            var unsortedCategories = await _categoryRepository.GetAllAsync(async query =>
            {
                if (!showHidden)
                    query = query.Where(c => c.Published);
                else if (overridePublished.HasValue)
                    query = query.Where(c => c.Published == overridePublished.Value);

                //apply ACL constraints
                if (!showHidden)
                {
                    var customer = await _workContext.GetCurrentCustomerAsync();
                }

                if (!string.IsNullOrWhiteSpace(categoryName))
                    query = query.Where(c => c.Name.Contains(categoryName));

                query = query.Where(c => !c.Deleted);

                return query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder).ThenBy(c => c.Id);
            });

            //sort categories
            var sortedCategories = await SortCategoriesForTreeAsync(unsortedCategories);

            //paging
            return new PagedList<NewsCategory>(sortedCategories, pageIndex, pageSize);
        }

        public async Task<IList<NewsCategory>> GetAllCategoriesByParentCategoryIdAsync(int parentCategoryId, bool showHidden = false)
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            var categories = await _categoryRepository.GetAllAsync(query =>
            {
                if (!showHidden)
                {
                    query = query.Where(c => c.Published);
                }

                query = query.Where(c => !c.Deleted && c.ParentCategoryId == parentCategoryId);

                return query.OrderBy(c => c.DisplayOrder).ThenBy(c => c.Id);
            }, cache => cache.PrepareKeyForDefaultCache(NopNewsCategoryDefaults.CategoriesByParentCategoryCacheKey,
                parentCategoryId, showHidden, customer));

            return categories;
        }

        public async Task<IList<NewsCategory>> GetCategoriesByIdsAsync(int[] categoryIds)
        {
            return await _categoryRepository.GetByIdsAsync(categoryIds, includeDeleted: false);
        }

        public async Task<IList<NewsCategory>> GetCategoryBreadCrumbAsync(NewsCategory category, IList<NewsCategory> allCategories = null, bool showHidden = false)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            var breadcrumbCacheKey = _staticCacheManager.PrepareKeyForDefaultCache(NopNewsCategoryDefaults.CategoryBreadcrumbCacheKey,
                category,
                await _customerService.GetCustomerRoleIdsAsync(await _workContext.GetCurrentCustomerAsync()),
                await _storeContext.GetCurrentStoreAsync(),
                await _workContext.GetWorkingLanguageAsync());

            return await _staticCacheManager.GetAsync(breadcrumbCacheKey, async () =>
            {
                var result = new List<NewsCategory>();

                //used to prevent circular references
                var alreadyProcessedCategoryIds = new List<int>();

                while (category != null && //not null
                       !category.Deleted && //not deleted
                       (showHidden || category.Published) && //published
                       !alreadyProcessedCategoryIds.Contains(category.Id)) //prevent circular references
                {
                    result.Add(category);

                    alreadyProcessedCategoryIds.Add(category.Id);

                    category = allCategories != null
                        ? allCategories.FirstOrDefault(c => c.Id == category.ParentCategoryId)
                        : await GetCategoryByIdAsync(category.ParentCategoryId);
                }

                result.Reverse();

                return result;
            });
        }

        public async Task<NewsCategory> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId, cache => default);
        }

        public async Task<IList<int>> GetChildCategoryIdsAsync(int parentCategoryId, int storeId = 0, bool showHidden = false)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(NopNewsCategoryDefaults.CategoriesChildIdsCacheKey,
                parentCategoryId,
                await _customerService.GetCustomerRoleIdsAsync(await _workContext.GetCurrentCustomerAsync()),
                storeId,
                showHidden);

            return await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                //little hack for performance optimization
                //there's no need to invoke "GetAllCategoriesByParentCategoryId" multiple times (extra SQL commands) to load childs
                //so we load all categories at once (we know they are cached) and process them server-side
                var categoriesIds = new List<int>();
                var categories = (await GetAllCategoriesAsync(storeId: storeId, showHidden: showHidden))
                    .Where(c => c.ParentCategoryId == parentCategoryId)
                    .Select(c => c.Id)
                    .ToList();
                categoriesIds.AddRange(categories);
                categoriesIds.AddRange(await categories.SelectManyAwait(async cId => await GetChildCategoryIdsAsync(cId, storeId, showHidden)).ToListAsync());

                return categoriesIds;
            });
        }

        public async Task<string> GetFormattedBreadCrumbAsync(NewsCategory category, IList<NewsCategory> allCategories = null, string separator = ">>", int languageId = 0)
        {
            var result = string.Empty;

            var breadcrumb = await GetCategoryBreadCrumbAsync(category, allCategories, true);
            for (var i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var categoryName = await _localizationService.GetLocalizedAsync(breadcrumb[i], x => x.Name, languageId);
                result = string.IsNullOrEmpty(result) ? categoryName : $"{result} {separator} {categoryName}";
            }

            return result;
        }

        public async Task<IPagedList<NewsInCategory>> GetNewsInCategoriesByCategoryIdAsync(int categoryId, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            if (categoryId == 0)
                return new PagedList<NewsInCategory>(new List<NewsInCategory>(), pageIndex, pageSize);

            var query = from pc in _newsInCategoryRepository.Table
                        join p in _newsRepository.Table on pc.NewsId equals p.Id
                        where pc.NewsCategoryId == categoryId
                        orderby pc.Id
                        select pc;

            if (!showHidden)
            {
                var categoriesQuery = _categoryRepository.Table.Where(c => c.Published);

                //apply ACL constraints
                var customer = await _workContext.GetCurrentCustomerAsync();

                query = query.Where(pc => categoriesQuery.Any(c => c.Id == pc.NewsCategoryId));
            }

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        public async Task<IList<NewsInCategory>> GetNewsInCategoriesByNewsIdAsync(int newsId, bool showHidden = false)
        {
            return await GetNewsInCategoriesByNewsIdAsync(newsId, (await _storeContext.GetCurrentStoreAsync()).Id, showHidden);
        }

        public async Task<IDictionary<int, int[]>> GetNewsInCategoryIdsAsync(int[] productIds)
        {
            var query = _newsInCategoryRepository.Table;

            return (await query.Where(p => productIds.Contains(p.NewsId))
                .Select(p => new { p.NewsId, p.NewsCategoryId })
                .ToListAsync())
                .GroupBy(a => a.NewsId)
                .ToDictionary(items => items.Key, items => items.Select(a => a.NewsCategoryId).ToArray());
        }

        public async Task<string[]> GetNotExistingCategoriesAsync(string[] categoryIdsNames)
        {
            if (categoryIdsNames == null)
                throw new ArgumentNullException(nameof(categoryIdsNames));

            var query = _categoryRepository.Table;
            var queryFilter = categoryIdsNames.Distinct().ToArray();
            //filtering by name
            var filter = await query.Select(c => c.Name)
                .Where(c => queryFilter.Contains(c))
                .ToListAsync();

            queryFilter = queryFilter.Except(filter).ToArray();

            //if some names not found
            if (!queryFilter.Any())
                return queryFilter.ToArray();

            //filtering by IDs
            filter = await query.Select(c => c.Id.ToString())
                .Where(c => queryFilter.Contains(c))
                .ToListAsync();

            return queryFilter.Except(filter).ToArray();
        }

        public async Task<NewsInCategory> GetNewsInCategoryByIdAsync(int newsCategoryId)
        {
            return await _newsInCategoryRepository.GetByIdAsync(newsCategoryId, cache => default);
        }

        public async Task InsertCategoryAsync(NewsCategory category)
        {
            await _categoryRepository.InsertAsync(category);
        }

        public async Task InsertNewsInCategoryAsync(NewsInCategory newsInCategory)
        {
            await _newsInCategoryRepository.InsertAsync(newsInCategory);
        }

        public async Task UpdateCategoryAsync(NewsCategory category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            //validate category hierarchy
            var parentCategory = await GetCategoryByIdAsync(category.ParentCategoryId);
            while (parentCategory != null)
            {
                if (category.Id == parentCategory.Id)
                {
                    category.ParentCategoryId = 0;
                    break;
                }

                parentCategory = await GetCategoryByIdAsync(parentCategory.ParentCategoryId);
            }

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task UpdateNewsInCategoryAsync(NewsInCategory newsInCategory)
        {
            await _newsInCategoryRepository.UpdateAsync(newsInCategory);
        }

        protected virtual async Task<IList<NewsCategory>> SortCategoriesForTreeAsync(IList<NewsCategory> source, int parentId = 0,
            bool ignoreCategoriesWithoutExistingParent = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var result = new List<NewsCategory>();

            foreach (var cat in source.Where(c => c.ParentCategoryId == parentId).ToList())
            {
                result.Add(cat);
                result.AddRange(await SortCategoriesForTreeAsync(source, cat.Id, true));
            }

            if (ignoreCategoriesWithoutExistingParent || result.Count == source.Count)
                return result;

            //find categories without parent in provided category source and insert them into result
            foreach (var cat in source)
                if (result.FirstOrDefault(x => x.Id == cat.Id) == null)
                    result.Add(cat);

            return result;
        }

        protected virtual async Task<IList<NewsInCategory>> GetNewsInCategoriesByNewsIdAsync(int newsId, int storeId,
            bool showHidden = false)
        {
            if (newsId == 0)
                return new List<NewsInCategory>();

            var customer = await _workContext.GetCurrentCustomerAsync();

            return await _newsInCategoryRepository.GetAllAsync(async query =>
            {
                if (!showHidden)
                {
                    var categoriesQuery = _categoryRepository.Table.Where(c => c.Published);

                    query = query.Where(pc => categoriesQuery.Any(c => c.Id == pc.NewsCategoryId));
                }

                return query
                    .Where(pc => pc.NewsId == newsId)
                    .OrderBy(pc => pc.Id);

            }, cache => _staticCacheManager.PrepareKeyForDefaultCache(NopNewsCategoryDefaults.NewsInCategoriesByNewsCacheKey,
                newsId, showHidden, customer));
        }

        public async Task<IList<NewsCategory>> GetCategoriesByNewsId(int newsId)
        {
            var lst = new List<NewsCategory>();

            var newsInCategories = await GetNewsInCategoriesByNewsIdAsync(newsId);

            foreach (var item in newsInCategories)
            {
                var category = await _categoryRepository.GetByIdAsync(item.Id);

                if (category != null)
                {
                    lst.Add(category);
                }
            }

            return lst;
        }
    }
}
