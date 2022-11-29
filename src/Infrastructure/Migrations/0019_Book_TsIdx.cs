using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(19)]
public class Book_TsIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE INDEX ts_idx ON books USING GIN(ts)");
    }
}