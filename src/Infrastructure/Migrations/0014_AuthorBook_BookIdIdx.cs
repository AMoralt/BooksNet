using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(14)]
public class AuthorBook_BookIdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("AuthorBook_BookIdIdx")
            .OnTable("author_book")
            .InSchema("public")
            .OnColumn("book_id");
    }
}