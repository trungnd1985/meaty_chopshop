using Nop.Web.Framework.UI.Paging;
using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.News
{
    public record NewsPagableModel : BasePageableModel
    {
        #region Properties        

        /// <summary>
        /// Gets or sets the products
        /// </summary>
        public IList<NewsItemModel> News { get; set; }

        #endregion

        #region Ctor

        public NewsPagableModel()
        {
            News = new List<NewsItemModel>();
        }

        #endregion
    }
}
