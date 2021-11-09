using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Builders.News
{
    public partial class NewsCategoryBuilder : NopEntityBuilder<NewsCategory>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                 .WithColumn(nameof(NewsCategory.Name)).AsString(400).NotNullable()
                 .WithColumn(nameof(NewsCategory.MetaKeywords)).AsString(400).Nullable()
                 .WithColumn(nameof(NewsCategory.MetaTitle)).AsString(400).Nullable();
        }
    }
}
