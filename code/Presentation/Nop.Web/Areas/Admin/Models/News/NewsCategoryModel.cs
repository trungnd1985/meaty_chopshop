using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.News
{
    public record NewsCategoryModel : BaseNopEntityModel,
        ILocalizedModel<NewsCategoryLocalizedModel>
    {
        public NewsCategoryModel()
        {
            //if (PageSize < 1)
            //{
            //    PageSize = 5;
            //}

            //Locales = new List<CategoryLocalizedModel>();
            //AvailableCategoryTemplates = new List<SelectListItem>();
            //AvailableCategories = new List<SelectListItem>();
            //AvailableDiscounts = new List<SelectListItem>();
            //SelectedDiscountIds = new List<int>();

            //SelectedCustomerRoleIds = new List<int>();
            //AvailableCustomerRoles = new List<SelectListItem>();

            //SelectedStoreIds = new List<int>();
            //AvailableStores = new List<SelectListItem>();

            //CategoryProductSearchModel = new CategoryProductSearchModel();
        }



        #region Properties        

        [NopResourceDisplayName("Admin.News.Categories.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.Description")]
        public string Description { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.MetaKeywords")]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.MetaDescription")]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.MetaTitle")]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.SeName")]
        public string SeName { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.Parent")]
        public int ParentCategoryId { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Admin.News.Categories.Fields.Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.PageSize")]
        public int PageSize { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.Published")]
        public bool Published { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.Deleted")]
        public bool Deleted { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        public IList<NewsCategoryLocalizedModel> Locales { get; set; }

        public string Breadcrumb { get; set; }

        public CategoryNewsSearchModel CategoryProductSearchModel { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        #endregion
    }

    public partial record NewsCategoryLocalizedModel : ILocalizedLocaleModel
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.Description")]
        public string Description { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.MetaKeywords")]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.MetaDescription")]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.MetaTitle")]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.SeName")]
        public string SeName { get; set; }
    }
}
