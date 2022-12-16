using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(20)]
public class Events : Migration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE if not exists events(
            id BIGSERIAL PRIMARY KEY,
            isbn VARCHAR(13) NOT NULL,
            name VARCHAR(100) NOT NULL,
            quantity INTEGER NOT NULL,
            price INTEGER NOT NULL,
            created_at TIMESTAMP NOT NULL DEFAULT NOW())");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists events");
    }
}