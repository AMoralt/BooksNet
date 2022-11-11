using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(4)]
public class BookFormats : Migration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE if not exists book_formats(
            id SERIAL PRIMARY KEY,
            name VARCHAR(200) UNIQUE NOT NULL)");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists book_formats");
    }
}