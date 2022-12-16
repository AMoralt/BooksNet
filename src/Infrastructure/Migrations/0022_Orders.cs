using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(22)]
public class Orders: Migration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE if not exists orders(
            id SERIAL PRIMARY KEY,
            date TIMESTAMP WITH TIME ZONE NOT NULL)");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists orders");
    }
}