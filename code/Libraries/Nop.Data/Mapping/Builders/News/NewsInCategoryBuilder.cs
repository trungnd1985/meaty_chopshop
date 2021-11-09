using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.News;
using Nop.Data.Extensions;

namespace Nop.Data.Mapping.Builders.News
{
    public partial class NewsInCategoryBuilder : NopEntityBuilder<NewsInCategory>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(NewsInCategory.NewsCategoryId)).AsInt32().ForeignKey<NewsCategory>()
                .WithColumn(nameof(NewsInCategory.NewsId)).AsInt32().ForeignKey<NewsItem>();
        }

        #endregion
    }
}
