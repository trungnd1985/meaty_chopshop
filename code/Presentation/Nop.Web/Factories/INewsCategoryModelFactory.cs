using Nop.Core.Domain.News;
using Nop.Web.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Factories
{
    public interface INewsCategoryModelFactory
    {
        Task<NewsCategoryModel> PrepareCategoryModelAsync(NewsCategory category);

    }
}
