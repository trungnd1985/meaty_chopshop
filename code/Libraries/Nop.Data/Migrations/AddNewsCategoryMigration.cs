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
        private readonly IMigrationManager _migrationManager;

        public AddNewsCategoryMigration(INopDataProvider dataProvider, IMigrationManager migrationManager)
        {
            _dataProvider = dataProvider;
            _migrationManager = migrationManager;
        }

        public override void Up()
        {
            _migrationManager.BuildTable<NewsCategory>(Create);
            _migrationManager.BuildTable<NewsInCategory>(Create);

        }

        public override void Down()
        {

        }
    }
}
