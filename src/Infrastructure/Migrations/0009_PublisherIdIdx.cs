using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(9)]
public class PublisherIdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("PublisherIdIdx")
            .OnTable("publishers")
            .InSchema("public")
            .OnColumn("id");
    }
}