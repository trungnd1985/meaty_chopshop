using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Web.Areas.Admin.Factories;
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

        #endregion

        public NewsCategoryController()
        {

        }

        [CheckLanguageSeoCode(true)]
        public IActionResult Index(int categoryId)
        {
            var model = new NewsCategoryModel();

            return View(model);
        }
    }
}
