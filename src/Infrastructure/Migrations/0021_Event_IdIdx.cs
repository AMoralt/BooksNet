using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(21)]
public class Event_IdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("Event_IdIdx")
            .OnTable("events")
            .InSchema("public")
            .OnColumn("id");
    }
}