using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(3)]
public class AuthorTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE if not exists authors(
            id SERIAL PRIMARY KEY,
            firstname VARCHAR(50) NOT NULL,
            lastname VARCHAR(50) NOT NULL,
            UNIQUE(firstname, lastname)
            )");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists authors");
    }
}