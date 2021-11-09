using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.StoreLocation.Components
{
    [ViewComponent(Name = "WidgetsStoreLocation")]
    public class WidgetsStoreLocationViewComponent : NopViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Plugins/Widgets.StoreLocation/Views/PublicInfo.cshtml");
        }
    }
}
