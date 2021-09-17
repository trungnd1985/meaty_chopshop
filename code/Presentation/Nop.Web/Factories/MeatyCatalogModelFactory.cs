using AutoMapper;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Seo;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Infrastructure.Cache;
using Nop.Web.Models.Catalog;
using Nop.Web.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Factories
{
    public class MeatyCatalogModelFactory : IMeatyCatalogModelFactory
    {
        #region Fields

        private readonly CatalogSettings _catalogSettings;
        private readonly ICategoryService _categoryService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly IProductModelFactory _productModelFactory;
        private readonly IProductService _productService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IStoreContext _storeContext;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly MediaSettings _mediaSettings;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ICustomerService _customerService;
        private readonly IMeatyProductService _meatyProductService;

        #endregion

        #region Ctor

        public MeatyCatalogModelFactory(
            CatalogSettings catalogSettings,
            ICategoryService categoryService,
            ICustomerService customerService,
            ILocalizationService localizationService,
            IPictureService pictureService,
            IProductModelFactory productModelFactory,
            IProductService productService,
            IStaticCacheManager staticCacheManager,
            IStoreContext storeContext,
            IWebHelper webHelper,
            IWorkContext workContext,
            IUrlRecordService urlRecordService,
            MediaSettings mediaSettings,
            IMeatyProductService meatyProductService)
        {
            _catalogSettings = catalogSettings;
            _categoryService = categoryService;
            _localizationService = localizationService;
            _pictureService = pictureService;
            _productModelFactory = productModelFactory;
            _productService = productService;
            _staticCacheManager = staticCacheManager;
            _storeContext = storeContext;
            _webHelper = webHelper;
            _workContext = workContext;
            _mediaSettings = mediaSettings;
            _urlRecordService = urlRecordService;
            _customerService = customerService;
            _meatyProductService = meatyProductService;
        }

        #endregion

        public async Task<List<CategoryModel>> PrepareHomepageCategoryModelsAsync()
        {
            var language = await _workContext.GetWorkingLanguageAsync();
            var customer = await _workContext.GetCurrentCustomerAsync();
            var customerRoleIds = await _customerService.GetCustomerRoleIdsAsync(customer);
            var store = await _storeContext.GetCurrentStoreAsync();
            var pictureSize = _mediaSettings.CategoryThumbPictureSize;
            var categoriesCacheKey = _staticCacheManager.PrepareKeyForDefaultCache(NopModelCacheDefaults.CategoryHomepageKey,
                store, customerRoleIds, pictureSize, language, _webHelper.IsCurrentConnectionSecured());

            var model = await _staticCacheManager.GetAsync(categoriesCacheKey, async () =>
            {
                var homepageCategories = await _categoryService.GetAllCategoriesDisplayedOnHomepageAsync();
                return await homepageCategories.SelectAwait(async category =>
                {
                    var catModel = new CategoryModel
                    {
                        Id = category.Id,
                        Name = await _localizationService.GetLocalizedAsync(category, x => x.Name),
                        Description = await _localizationService.GetLocalizedAsync(category, x => x.Description),
                        MetaKeywords = await _localizationService.GetLocalizedAsync(category, x => x.MetaKeywords),
                        MetaDescription = await _localizationService.GetLocalizedAsync(category, x => x.MetaDescription),
                        MetaTitle = await _localizationService.GetLocalizedAsync(category, x => x.MetaTitle),
                        SeName = await _urlRecordService.GetSeNameAsync(category),
                    };

                    var products = await _meatyProductService.GetAllProductsDisplayedOnHomepageAsync(category.Id);

                    //prepare picture model
                    var secured = _webHelper.IsCurrentConnectionSecured();
                    var categoryPictureCacheKey = _staticCacheManager.PrepareKeyForDefaultCache(NopModelCacheDefaults.CategoryPictureModelKey,
                        category, pictureSize, true, language, secured, store);
                    catModel.PictureModel = await _staticCacheManager.GetAsync(categoryPictureCacheKey, async () =>
                    {
                        var picture = await _pictureService.GetPictureByIdAsync(category.PictureId);
                        string fullSizeImageUrl, imageUrl;

                        (fullSizeImageUrl, picture) = await _pictureService.GetPictureUrlAsync(picture);
                        (imageUrl, _) = await _pictureService.GetPictureUrlAsync(picture, pictureSize);

                        var titleLocale = await _localizationService.GetResourceAsync("Media.Category.ImageLinkTitleFormat");
                        var altLocale = await _localizationService.GetResourceAsync("Media.Category.ImageAlternateTextFormat");
                        return new PictureModel
                        {
                            FullSizeImageUrl = fullSizeImageUrl,
                            ImageUrl = imageUrl,
                            Title = string.Format(titleLocale, catModel.Name),
                            AlternateText = string.Format(altLocale, catModel.Name)
                        };
                    });
                    catModel.SubCategories = await (await _categoryService.GetAllCategoriesByParentCategoryIdAsync(category.Id))
                        .SelectAwait(async curCategory =>
                        {
                            var subCatModel = new CategoryModel.SubCategoryModel
                            {
                                Id = curCategory.Id,
                                Name = await _localizationService.GetLocalizedAsync(curCategory, y => y.Name),
                                SeName = await _urlRecordService.GetSeNameAsync(curCategory),
                                Description = await _localizationService.GetLocalizedAsync(curCategory, y => y.Description)
                            };

                            return subCatModel;
                        }).ToListAsync();

                    catModel.FeaturedProducts = (await _productModelFactory.PrepareProductOverviewModelsAsync(products, category: catModel)).ToList();

                    return catModel;
                }).ToListAsync();
            });

            return model;
        }
    }
}
