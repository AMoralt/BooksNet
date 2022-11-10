using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(10)]
public class GenreIdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("GenreIdIdx")
            .OnTable("genres")
            .InSchema("public")
            .OnColumn("id");
    }
}