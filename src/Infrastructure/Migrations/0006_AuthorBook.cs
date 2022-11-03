using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(6)]
public class AuthorBookTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE if not exists author_book(
            book_id INTEGER REFERENCES books(id) ON DELETE CASCADE,
            author_id INTEGER REFERENCES authors(id) ON DELETE CASCADE)");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists author_book");
    }
}