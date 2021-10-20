using FluentMigrator;

namespace Logeecom.DemoProjekat.Migrations
{
    [Migration(202101181545)]
    public class AddProductsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Products")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("SKU").AsString(32).NotNullable().Unique()
                .WithColumn("Title").AsString(32).NotNullable()
                .WithColumn("Brand").AsString(32).NotNullable()
                .WithColumn("Price").AsInt32().NotNullable()
                .WithColumn("ShortDescription").AsString(256).NotNullable()
                .WithColumn("Description").AsString(512).NotNullable()
                .WithColumn("Image").AsString(128).NotNullable()
                .WithColumn("Enabled").AsBoolean().NotNullable()
                .WithColumn("Featured").AsBoolean().NotNullable()
                .WithColumn("ViewCount").AsInt32().NotNullable()
                .WithColumn("CategoryId").AsInt32().ForeignKey("Categories", "Id").OnDeleteOrUpdate(System.Data.Rule.Cascade);
        }
        public override void Down()
        {
            Delete.Table("Products");
        }
    }
}
