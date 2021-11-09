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

        Task<NewsCategoryListModel> PrepareCategoryListModelAsync(NewsCategorySearchModel searchModel);

        Task<NewsCategoryModel> PrepareCategoryModelAsync(NewsCategoryModel model, NewsCategory category, bool excludeProperties = false);

        Task<CategoryNewsListModel> PrepareCategoryNewsListModelAsync(CategoryNewsSearchModel searchModel, NewsCategory category);

        Task<AddNewsToCategorySearchModel> PrepareAddNewsToCategorySearchModelAsync(AddNewsToCategorySearchModel searchModel);

        Task<AddNewsToCategoryListModel> PrepareAddNewsToCategoryListModelAsync(AddNewsToCategorySearchModel searchModel);
    }
}
