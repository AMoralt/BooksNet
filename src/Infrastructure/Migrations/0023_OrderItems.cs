using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(23)]
public class OrderItems: Migration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE if not exists order_items(
            order_id INTEGER REFERENCES orders(id) ON DELETE CASCADE,
            book_id INTEGER REFERENCES books(id) ON DELETE CASCADE,
            quantity INTEGER NOT NULL,
            price INTEGER NOT NULL)");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists order_items");
    }
}