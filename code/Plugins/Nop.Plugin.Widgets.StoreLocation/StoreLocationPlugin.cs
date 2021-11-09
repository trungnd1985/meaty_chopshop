using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.StoreLocation
{
    public class StoreLocationPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IWebHelper _webHelper;

        public StoreLocationPlugin(IWebHelper webHelper)
        {
            _webHelper = webHelper;
        }

        public bool HideInWidgetList => throw new NotImplementedException();

        public string GetWidgetViewComponentName(string widgetZone)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            throw new NotImplementedException();
        }

        public override Task InstallAsync()
        {
            return base.InstallAsync();
        }

        public override Task UninstallAsync()
        {
            return base.UninstallAsync();
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsStoreLocation/Configure";
        }
    }
}
