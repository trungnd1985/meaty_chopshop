using FluentMigrator;
using FluentMigrator.Oracle;
using FluentMigrator.Postgres;
using FluentMigrator.SqlServer;
using Nop.Core.Domain.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Data.Extensions;

namespace Nop.Data.Migrations
{
    [NopMigration("2021-11-09 00:00:00", "4.40.0", UpdateMigrationType.Data)]
    public class AddNewsCategoryMigration : Migration
    {
        private readonly INopDataProvider _dataProvider;

        public AddNewsCategoryMigration(INopDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public override void Up()
        {
            var newsCategoryTableName = nameof(NewsCategory);
            var newsCategoryTable = Schema.Table(newsCategoryTableName);

            if (!newsCategoryTable.Exists())
            {
                Create.Table(newsCategoryTableName)
                    .WithColumn(nameof(NewsCategory.Id)).AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                    .WithColumn(nameof(NewsCategory.Name)).AsString(400).NotNullable()
                    .WithColumn(nameof(NewsCategory.Description)).AsString().Nullable()
                    .WithColumn(nameof(NewsCategory.CreatedOnUtc)).AsDateTime().NotNullable()
                    .WithColumn(nameof(NewsCategory.Deleted)).AsBoolean().NotNullable()
                    .WithColumn(nameof(NewsCategory.DisplayOrder)).AsInt32().NotNullable()
                    .WithColumn(nameof(NewsCategory.MetaDescription)).AsString(400).Nullable()
                    .WithColumn(nameof(NewsCategory.MetaTitle)).AsString(400).Nullable()
                    .WithColumn(nameof(NewsCategory.PageSize)).AsInt32().NotNullable()
                    .WithColumn(nameof(NewsCategory.ParentCategoryId)).AsInt32().NotNullable()
                    .WithColumn(nameof(NewsCategory.PictureId)).AsInt32().NotNullable()
                    .WithColumn(nameof(NewsCategory.Published)).AsBoolean().NotNullable()
                    .WithColumn(nameof(NewsCategory.SubjectToAcl)).AsBoolean().NotNullable()
                    .WithColumn(nameof(NewsCategory.UpdatedOnUtc)).AsDateTime().NotNullable()
                    ;
            }

            var newsInCategoryTableName = nameof(NewsInCategory);
            var newsInCategoryTable = Schema.Table(newsInCategoryTableName);

            if (!newsInCategoryTable.Exists())
            {
                Create.Table(newsInCategoryTableName)
                    .WithColumn(nameof(NewsInCategory.Id)).AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                    .WithColumn(nameof(NewsInCategory.NewsCategoryId)).AsInt32().NotNullable().ForeignKey<NewsCategory>()
                    .WithColumn(nameof(NewsInCategory.NewsId)).AsInt32().NotNullable().ForeignKey<NewsItem>();
            }

        }

        public override void Down()
        {

        }
    }
}
