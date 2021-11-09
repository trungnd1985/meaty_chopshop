using Nop.Core.Domain.News;
using Nop.Web.Areas.Admin.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Factories
{
    public partial interface INewsCategoryModelFactory
    {
        Task<NewsCategorySearchModel> PrepareCategorySearchModelAsync(NewsCategorySearchModel searchModel);

        Task<CategoryListModel> PrepareCategoryListModelAsync(NewsCategorySearchModel searchModel);

        Task<NewsCategoryModel> PrepareCategoryModelAsync(NewsCategoryModel model, NewsCategory category, bool excludeProperties = false);

        Task<CategoryProductListModel> PrepareCategoryProductListModelAsync(CategoryProductSearchModel searchModel, NewsCategory category);

        Task<AddProductToCategorySearchModel> PrepareAddProductToCategorySearchModelAsync(AddProductToCategorySearchModel searchModel);

        Task<AddProductToCategoryListModel> PrepareAddProductToCategoryListModelAsync(AddProductToCategorySearchModel searchModel);
    }
}
