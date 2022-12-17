using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(24)]
public class FillDictionaries2 : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql(@"
            INSERT INTO events (isbn, name, quantity, price, created_at)
            VALUES 
                (9785041575441,'DecreaseQuantityDomainEvent', 9970 , 249, '2022-11-24 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9428, 249, '2022-11-25 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9059, 249, '2022-11-26 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 8817, 249, '2022-11-27 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 8743, 249, '2022-11-28 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 8735, 249, '2022-11-29 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 8993, 249, '2022-11-30 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9363, 249, '2022-12-01 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9545, 249, '2022-12-02 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9676, 249, '2022-12-03 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9937, 249, '2022-12-04 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10139, 249, '2022-12-05 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10326, 249, '2022-12-06 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10359, 249, '2022-12-07 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10293, 249, '2022-12-08 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10240, 249, '2022-12-09 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10416, 249, '2022-12-10 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11225, 249, '2022-12-11 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11907, 249, '2022-12-12 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11812, 249, '2022-12-13 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11542, 249, '2022-12-14 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11149, 249, '2022-12-15 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10835, 249, '2022-12-16 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10335, 249, '2022-12-17 00:00:00.000000')
                ON CONFLICT DO NOTHING");
    }
}