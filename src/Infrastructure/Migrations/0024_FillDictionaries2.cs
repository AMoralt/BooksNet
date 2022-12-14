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
                (9785041575441,'DecreaseQuantityDomainEvent', 9444, 249, '2022-09-14 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9265, 249, '2022-09-15 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 8855, 249, '2022-09-22 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9970 , 249, '2022-09-24 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9428, 249, '2022-09-25 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9059, 249, '2022-09-26 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 8817, 249, '2022-09-27 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 8743, 249, '2022-10-28 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 8735, 249, '2022-10-29 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 8993, 249, '2022-10-30 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9363, 249, '2022-10-01 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9545, 249, '2022-10-02 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9676, 249, '2022-10-03 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 9937, 249, '2022-10-04 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10139, 249, '2022-11-05 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10326, 249, '2022-11-06 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10359, 249, '2022-11-07 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10293, 249, '2022-11-08 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10240, 249, '2022-11-09 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10416, 249, '2022-11-10 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11225, 249, '2022-12-11 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11907, 249, '2022-12-12 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11812, 249, '2022-12-13 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11542, 249, '2022-12-14 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11149, 249, '2022-12-15 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10835, 249, '2022-12-16 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10335, 249, '2022-12-17 00:00:00.000000'),
                
                (9785041575441,'DecreaseQuantityDomainEvent', 10139, 249, '2022-08-14 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11149, 249, '2022-08-15 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11907, 249, '2022-08-22 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10240 , 249, '2022-08-24 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11542, 249, '2022-08-25 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10835, 249, '2022-08-26 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10835, 249, '2022-08-27 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10359, 249, '2022-07-28 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11542, 249, '2022-07-29 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11149, 249, '2022-07-30 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10335, 249, '2022-07-01 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10240, 249, '2022-07-02 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10335, 249, '2022-07-03 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10139, 249, '2022-07-04 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10139, 249, '2022-06-05 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10326, 249, '2022-06-06 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10359, 249, '2022-06-07 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10293, 249, '2022-06-08 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10240, 249, '2022-06-09 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10416, 249, '2022-06-10 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11225, 249, '2022-05-11 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11907, 249, '2022-05-12 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11812, 249, '2022-05-13 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11542, 249, '2022-05-14 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 11149, 249, '2022-05-15 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10835, 249, '2022-05-16 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 10335, 249, '2022-05-17 00:00:00.000000'),
                
                (9785041575441,'DecreaseQuantityDomainEvent', 9444, 249, '2022-04-14 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-04-15 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-04-22 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000 , 249, '2022-04-24 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-04-25 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-04-26 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-04-27 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-03-28 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-03-29 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-03-30 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-03-01 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-03-02 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-03-03 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-03-04 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-02-05 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-02-06 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-02-07 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-02-08 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-02-09 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-02-10 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-01-11 00:00:00.000000'),
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-01-12 00:00:00.000000'),    
                (9785041575441,'DecreaseQuantityDomainEvent', 20000, 249, '2022-01-13 00:00:00.000000')
                ON CONFLICT DO NOTHING");
    }
}