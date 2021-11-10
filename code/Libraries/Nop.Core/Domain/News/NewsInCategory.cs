using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.News
{
    public class NewsInCategory : BaseEntity
    {
        public int NewsId { get; set; }

        public int NewsCategoryId { get; set; }

        //public int DisplayOrder { get; set; }
    }
}
