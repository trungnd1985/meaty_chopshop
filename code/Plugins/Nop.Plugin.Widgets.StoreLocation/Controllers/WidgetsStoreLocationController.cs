using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.StoreLocation.Models;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.StoreLocation.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class WidgetsStoreLocationController: BasePluginController
    {
        public WidgetsStoreLocationController()
        {

        }

        public IActionResult Configure()
        {
            return View("~/Plugins/Widgets.StoreLocation/Views/Configure.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            return await Configure();
        }
    }
}
