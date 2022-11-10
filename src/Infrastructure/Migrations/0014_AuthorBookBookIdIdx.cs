using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(14)]
public class AuthorBookBookIdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("AuthorBookBookIdIdx")
            .OnTable("author_book")
            .InSchema("public")
            .OnColumn("book_id");
    }
}