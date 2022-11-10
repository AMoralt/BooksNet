using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(11)]
public class Author_IdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("Author_IdIdx")
            .OnTable("authors")
            .InSchema("public")
            .OnColumn("id");
    }
}