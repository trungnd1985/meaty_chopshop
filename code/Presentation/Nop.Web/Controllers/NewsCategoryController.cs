using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.News;
using Nop.Services.News;
using Nop.Web.Factories;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Controllers
{
    public class NewsCategoryController : BasePublicController
    {
        #region Fields

        private readonly INewsCategoryModelFactory _newsCategoryModelFactory;
        private readonly INewsModelFactory _newsModelFactory;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly INewsCategoryService _newsCategoryService;
        private readonly INewsService _newsService;

        #endregion

        public NewsCategoryController(
            INewsCategoryService newsCategoryService
            , INewsCategoryModelFactory newsCategoryModelFactory
            , IWebHelper webHelper
            , INewsService newsService)
        {
            _newsCategoryService = newsCategoryService;
            _newsCategoryModelFactory = newsCategoryModelFactory;
            _webHelper = webHelper;
            _newsService = newsService;
        }

        [CheckLanguageSeoCode(true)]
        public async Task<IActionResult> Index(int categoryId)
        {
            var category = await _newsCategoryService.GetCategoryByIdAsync(categoryId);

            if (!CheckCategoryAvailability(category))
            {
                return InvokeHttp404();
            }

            var model = await _newsCategoryModelFactory.PrepareCategoryModelAsync(category);

            return View(model);
        }

        #region Utilities

        private bool CheckCategoryAvailability(NewsCategory category)
        {
            var isAvailable = true;

            if (category == null || category.Deleted)
                isAvailable = false;

            var notAvailable =
                //published?
                !category.Published;
            //Check whether the current user has a "Manage categories" permission (usually a store owner)
            //We should allows him (her) to use "Preview" functionality
            
            if (notAvailable)
                isAvailable = false;

            return isAvailable;
        }

        #endregion
    }
}
