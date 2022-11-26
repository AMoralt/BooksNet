using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(5)]
public class BookTable: Migration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE if not exists books(
            id SERIAL PRIMARY KEY,
            title TEXT NOT NULL,
            isbn VARCHAR(13) UNIQUE NOT NULL,
            quantity INTEGER NOT NULL,
            price INTEGER NOT NULL,
            publicationdate DATE NOT NULL,
            publisher_id INTEGER REFERENCES publishers(id) ON DELETE CASCADE,
            genre_id INTEGER REFERENCES genres(id) ON DELETE CASCADE,
            format_id INTEGER REFERENCES book_formats(id) ON DELETE CASCADE)");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists books");
    }
}