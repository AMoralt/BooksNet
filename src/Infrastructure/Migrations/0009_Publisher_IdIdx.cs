using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(9)]
public class Publisher_IdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("Publisher_IdIdx")
            .OnTable("publishers")
            .InSchema("public")
            .OnColumn("id");
    }
}