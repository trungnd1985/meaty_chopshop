﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.News
{
    public record AddNewsToCategorySearchModel : BaseSearchModel
    {
        #region Ctor

        public AddNewsToCategorySearchModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Admin.News.List.SearchNewsName")]
        public string SearchNewsName { get; set; }

        [NopResourceDisplayName("Admin.News.List.SearchCategory")]
        public int SearchCategoryId { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        #endregion
    }
}
