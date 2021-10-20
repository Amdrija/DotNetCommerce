using FluentMigrator;

namespace Logeecom.DemoProjekat.Migrations
{
    [Migration(202101181405)]
    public class AddCategoryTable : Migration
    {
        public override void Up()
        {
            Create.Table("Categories")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Code").AsString(32).NotNullable().Unique()
                .WithColumn("Title").AsString(32).NotNullable()
                .WithColumn("Description").AsString(256).NotNullable()
                .WithColumn("ParentId").AsInt32().Nullable().ForeignKey("Categories", "Id");
        }

        public override void Down()
        {
            Delete.Table("Categories");
        }
    }
}
