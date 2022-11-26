using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(10)]
public class Genre_IdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("Genre_IdIdx")
            .OnTable("genres")
            .InSchema("public")
            .OnColumn("id");
    }
}