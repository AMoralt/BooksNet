using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(16)]
public class Book_TitleTs: Migration
{
    public override void Up()
    {
        Execute.Sql(@"ALTER TABLE books
            ADD COLUMN if not exists title_ts tsvector
            GENERATED ALWAYS AS (to_tsvector('russian', title)) STORED");
    }

    public override void Down()
    {
        Execute.Sql(@"DROP TABLE if exists books");
    }
}