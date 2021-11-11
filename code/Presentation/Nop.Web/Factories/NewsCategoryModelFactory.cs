using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.News;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.News;
using Nop.Services.Seo;
using Nop.Web.Infrastructure.Cache;
using Nop.Web.Models.Media;
using Nop.Web.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Factories
{
    public class NewsCategoryModelFactory : INewsCategoryModelFactory
    {
        private readonly ILocalizationService _localizationService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly INewsCategoryService _newsCategoryService;
        private readonly INewsService _newsService;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly INewsModelFactory _newsModelFactory;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IPictureService _pictureService;
        private readonly MediaSettings _mediaSettings;

        public NewsCategoryModelFactory(
            ILocalizationService localizationService
            , IUrlRecordService urlRecordService
            , INewsCategoryService newsCategoryService
            , INewsService newsService
            , IWebHelper webHelper
            , IWorkContext workContext
            , INewsModelFactory newsModelFactory
            , IStaticCacheManager staticCacheManager
            , IPictureService pictureService
            , MediaSettings mediaSettings)
        {
            _localizationService = localizationService;
            _urlRecordService = urlRecordService;
            _newsCategoryService = newsCategoryService;
            _newsService = newsService;
            _webHelper = webHelper;
            _workContext = workContext;
            _newsModelFactory = newsModelFactory;
            _staticCacheManager = staticCacheManager;
            _pictureService = pictureService;
            _mediaSettings = mediaSettings;
        }

        public async Task<NewsCategoryModel> PrepareCategoryModelAsync(NewsCategory category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            var model = new NewsCategoryModel
            {
                Id = category.Id,
                Name = await _localizationService.GetLocalizedAsync(category, x => x.Name),
                Description = await _localizationService.GetLocalizedAsync(category, x => x.Description),
                MetaKeywords = await _localizationService.GetLocalizedAsync(category, x => x.MetaKeywords),
                MetaDescription = await _localizationService.GetLocalizedAsync(category, x => x.MetaDescription),
                MetaTitle = await _localizationService.GetLocalizedAsync(category, x => x.MetaTitle),
                SeName = await _urlRecordService.GetSeNameAsync(category),
                SubCategories = await PrepareCategoryProductsModelAsync(category, command)
            };

            //category breadcrumb
            model.DisplayCategoryBreadcrumb = true;

            model.CategoryBreadcrumb = await (await _newsCategoryService.GetCategoryBreadCrumbAsync(category)).SelectAwait(async catBr =>
                new NewsCategoryModel
                {
                    Id = catBr.Id,
                    Name = await _localizationService.GetLocalizedAsync(catBr, x => x.Name),
                    SeName = await _urlRecordService.GetSeNameAsync(catBr)
                }).ToListAsync();                                                                                                                                           

            var pictureSize = _mediaSettings.CategoryThumbPictureSize;

            //subcategories
            model.SubCategories = await(await _newsCategoryService.GetAllCategoriesByParentCategoryIdAsync(category.Id))
                .SelectAwait(async curCategory =>
                {
                    var subCatModel = new NewsCategoryModel.SubCategoryModel
                    {
                        Id = curCategory.Id,
                        Name = await _localizationService.GetLocalizedAsync(curCategory, y => y.Name),
                        SeName = await _urlRecordService.GetSeNameAsync(curCategory),
                        Description = await _localizationService.GetLocalizedAsync(curCategory, y => y.Description)
                    };

                    //prepare picture model
                    var categoryPictureCacheKey = _staticCacheManager.PrepareKeyForDefaultCache(NopModelCacheDefaults.NewsCategoryPictureModelKey, curCategory,
                        pictureSize, true, await _workContext.GetWorkingLanguageAsync(), _webHelper.IsCurrentConnectionSecured());

                    subCatModel.PictureModel = await _staticCacheManager.GetAsync(categoryPictureCacheKey, async () =>
                    {
                        var picture = await _pictureService.GetPictureByIdAsync(curCategory.PictureId);
                        string fullSizeImageUrl, imageUrl;

                        (fullSizeImageUrl, picture) = await _pictureService.GetPictureUrlAsync(picture);
                        (imageUrl, _) = await _pictureService.GetPictureUrlAsync(picture, pictureSize);

                        var pictureModel = new PictureModel
                        {
                            FullSizeImageUrl = fullSizeImageUrl,
                            ImageUrl = imageUrl,
                            Title = string.Format(await _localizationService
                                .GetResourceAsync("Media.Category.ImageLinkTitleFormat"), subCatModel.Name),
                            AlternateText = string.Format(await _localizationService
                                .GetResourceAsync("Media.Category.ImageAlternateTextFormat"), subCatModel.Name)
                        };

                        return pictureModel;
                    });

                    return subCatModel;
                }).ToListAsync();

            //featured products
            var featuredProducts = await _productService.GetCategoryFeaturedProductsAsync(category.Id);
            if (featuredProducts != null)
                model.NewsItemListModel = (await _newsModelFactory.PrepareProductOverviewModelsAsync(featuredProducts)).ToList();

            return model;
        }
    }
}
