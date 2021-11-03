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
        public bool HideInWidgetList => throw new NotImplementedException();

        public string GetWidgetViewComponentName(string widgetZone)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
