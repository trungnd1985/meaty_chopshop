using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Migrations
{
    [NopMigration("2021-11-13 00:00:00", "4.40.0", UpdateMigrationType.Data)]
    public class AddDisplayAsComboMigration : Migration
    {
        private readonly INopDataProvider _dataProvider;
        private readonly IMigrationManager _migrationManager;

        public AddDisplayAsComboMigration(INopDataProvider dataProvider, IMigrationManager migrationManager)
        {
            _dataProvider = dataProvider;
            _migrationManager = migrationManager;
        }

        public override void Up()
        {
            Create.Column("DisplayAsCombo").OnTable("Product").AsBoolean().Nullable();
        }

        public override void Down()
        {
            Delete.Column("DisplayAsCombo").FromTable("Product");
        }
    }
}
