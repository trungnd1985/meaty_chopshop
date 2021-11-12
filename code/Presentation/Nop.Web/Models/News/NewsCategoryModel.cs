using Nop.Web.Framework.Models;
using Nop.Web.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Nop.Web.Models.Catalog.CategoryModel;

namespace Nop.Web.Models.News
{
    public record NewsCategoryModel : BaseNopEntityModel
    {
        public NewsCategoryModel()
        {
            PictureModel = new PictureModel();
            SubCategories = new List<SubCategoryModel>();
            CategoryBreadcrumb = new List<NewsCategoryModel>();
            NewsItems = new NewsPagableModel();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }

        public PictureModel PictureModel { get; set; }

        public bool DisplayCategoryBreadcrumb { get; set; }
        public IList<NewsCategoryModel> CategoryBreadcrumb { get; set; }

        public IList<SubCategoryModel> SubCategories { get; set; }        

        public NewsPagableModel NewsItems { get; set; }
    }
}
