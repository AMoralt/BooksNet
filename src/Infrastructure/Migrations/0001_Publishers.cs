using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(1)]
public class PublisherTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE if not exists publishers(
            id SERIAL PRIMARY KEY,
            name VARCHAR(100) UNIQUE NOT NULL)");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists publishers");
    }
}