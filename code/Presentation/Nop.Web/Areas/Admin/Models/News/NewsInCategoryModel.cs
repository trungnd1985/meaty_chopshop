using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.News
{
    public record NewsInCategoryModel : BaseNopEntityModel
    {
        #region Properties

        public int CategoryId { get; set; }

        public int NewsId { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.Title")]
        public string Title { get; set; }

        [NopResourceDisplayName("Admin.News.Categories.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        #endregion
    }
}
