using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(18)]
public class Book_Ts : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql(@"ALTER TABLE books ADD COLUMN if not exists ts tsvector
                GENERATED ALWAYS AS ( to_tsvector('russian', title)) STORED");
    }
}