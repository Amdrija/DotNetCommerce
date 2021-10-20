using FluentMigrator;

namespace Logeecom.DemoProjekat.Migrations
{
    [Migration(202101181507)]
    public class AddUserTable : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Username").AsString(32).NotNullable()
                .WithColumn("Password").AsString(64).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
