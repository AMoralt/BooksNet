using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(12)]
public class BookFormat_IdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("BookFormat_IdIdx")
            .OnTable("book_formats")
            .InSchema("public")
            .OnColumn("id");
    }
}