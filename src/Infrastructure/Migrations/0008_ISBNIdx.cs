using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(8)]
public class ISBNIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("ISBNIdx")
            .OnTable("books")
            .InSchema("public")
            .OnColumn("isbn");
    }
}