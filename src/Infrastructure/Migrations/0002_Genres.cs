using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(2)]
public class GenreTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE if not exists genres(
            id SERIAL PRIMARY KEY,
            name VARCHAR(50) UNIQUE NOT NULL)");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists genres");
    }
}