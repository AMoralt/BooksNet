using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(13)]
public class Book_IdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("Book_IdIdx")
            .OnTable("books")
            .InSchema("public")
            .OnColumn("id");
    }
}