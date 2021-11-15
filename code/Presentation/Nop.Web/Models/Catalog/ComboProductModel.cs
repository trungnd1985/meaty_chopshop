using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Catalog
{
    public record ComboProductModel : BaseNopModel
    {
        public ComboProductModel()
        {
            Products = new List<ProductOverviewModel>();
        }

        public List<ProductOverviewModel> Products { get; set; }
    }
}
