﻿using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.News
{
    public record AddNewsToCategoryListModel : BasePagedListModel<NewsItemModel>
    {
    }
}
