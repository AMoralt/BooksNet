using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(8)]
public class ISBN_Idx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("ISBN_Idx")
            .OnTable("books")
            .InSchema("public")
            .OnColumn("isbn");
    }
}