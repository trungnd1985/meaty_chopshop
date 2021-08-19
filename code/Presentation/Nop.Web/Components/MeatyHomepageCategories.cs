using Microsoft.AspNetCore.Mvc;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Components
{
    public class MeatyHomepageCategoriesViewComponent : NopViewComponent
    {
        private readonly IMeatyCatalogModelFactory _catalogModelFactory;

        public MeatyHomepageCategoriesViewComponent(IMeatyCatalogModelFactory catalogModelFactory)
        {
            _catalogModelFactory = catalogModelFactory;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _catalogModelFactory.PrepareHomepageCategoryModelsAsync();
            if (!model.Any())
                return Content("");

            return View(model);
        }
    }
}
