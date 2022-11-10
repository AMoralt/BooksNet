using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(13)]
public class BookIdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("BookIdIdx")
            .OnTable("books")
            .InSchema("public")
            .OnColumn("id");
    }
}