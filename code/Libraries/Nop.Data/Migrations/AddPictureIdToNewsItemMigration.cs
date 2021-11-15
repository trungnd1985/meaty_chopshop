using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Migrations
{
    [NopMigration("2021-11-15 00:00:00", "4.40.0", UpdateMigrationType.Data)]
    public class AddPictureIdToNewsItemMigration: Migration
    {
        private readonly INopDataProvider _dataProvider;
        private readonly IMigrationManager _migrationManager;

        public AddPictureIdToNewsItemMigration(INopDataProvider dataProvider, IMigrationManager migrationManager)
        {
            _dataProvider = dataProvider;
            _migrationManager = migrationManager;
        }

        public override void Up()
        {
            Create.Column("PictureId").OnTable("News").AsInt32().WithDefaultValue(0);
        }

        public override void Down()
        {
            Delete.Column("PictureId").FromTable("News");
        }
    }
}
