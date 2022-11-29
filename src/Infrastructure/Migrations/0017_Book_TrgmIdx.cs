using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(17)]
public class Book_TrgmIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE INDEX if not exists Book_TrgmIdx ON books USING GIN(title gin_trgm_ops)");
    }
}