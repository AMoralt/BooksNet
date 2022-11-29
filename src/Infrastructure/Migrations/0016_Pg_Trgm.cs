using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(16)]
public class Pg_Trgm : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE EXTENSION pg_trgm");
    }
}