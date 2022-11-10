using FluentMigrator;

namespace EmptyProjectASPNETCORE.Migrations;

[Migration(12)]
public class BookFormatIdIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Index("BookFormatIdIdx")
            .OnTable("book_formats")
            .InSchema("public")
            .OnColumn("id");
    }
}