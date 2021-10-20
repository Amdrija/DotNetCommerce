using FluentMigrator;

namespace Logeecom.DemoProjekat.Migrations
{
    [Migration(202101181538)]
    public class InsertAdminRow : Migration
    {
        
        public override void Up()
        {
            Insert.IntoTable("Users")
                .Row(new { Username = "andrija", Password = "3eaf0ff39b53c1e893971406e247f70fcad2b1d40674d696eea726e6953089c7" });
        }

        public override void Down()
        {
            Delete.FromTable("Users")
                .Row(new { Username = "andrija" });
        }
    }
}
