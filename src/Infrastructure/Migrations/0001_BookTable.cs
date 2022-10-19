using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(1)]
public class BookTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE books()");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists books");
    }
}