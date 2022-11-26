using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(15)]
public class AuthorBook_AuthorIdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("AuthorBook_AuthorIdIdx")
            .OnTable("author_book")
            .InSchema("public")
            .OnColumn("author_id");
    }
}