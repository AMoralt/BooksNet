using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(11)]
public class AuthorIdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("AuthorIdIdx")
            .OnTable("authors")
            .InSchema("public")
            .OnColumn("id");
    }
}