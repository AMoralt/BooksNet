using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(15)]
public class AuthorBookAuthorIdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("AuthorBookAuthorIdIdx")
            .OnTable("author_book")
            .InSchema("public")
            .OnColumn("author_id");
    }
}