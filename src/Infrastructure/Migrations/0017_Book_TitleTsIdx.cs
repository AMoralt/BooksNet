using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(17)]
public class Book_TitleTsIdx : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE INDEX if not exists Book_TitleTsIdx ON books USING GIN (title_ts)");
    }
}