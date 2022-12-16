using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(7)]
public class FillDictionaries : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql(@"
            INSERT INTO book_formats (name)
            VALUES 
                ('твердая бумажная'),
                ('мягкая бумажная'),
                ('электронная книга'),
                ('аудио-книга'),
                ('журнал'),
                ('газета'),
                ('комикс'),
                ('интегральная'),
                ('мягкая глянцевая')
            ON CONFLICT DO NOTHING");
        
        Execute.Sql(@"
            INSERT INTO genres (name)
            VALUES 
                ('фантастика'),
                ('драма'),
                ('экшн'),
                ('приключение'),
                ('роман'),
                ('мистика'),
                ('хоррор'),
                ('проза'),
                ('романтика'),
                ('детектив'),
                ('сказки'),
                ('дневник'),
                ('эзотерика')
            ON CONFLICT DO NOTHING");
        
        Execute.Sql(@"
            INSERT INTO publishers (name)
            VALUES
                ('эксмо'),
                ('аст'),
                ('азбука'),
                ('росмэн')
            ON CONFLICT DO NOTHING");

        Execute.Sql(@"
            INSERT INTO authors (firstname, lastname)
            VALUES
                ('татьяна', 'алюшина'),
                ('валерия', 'вербинина'),
                ('валентина', 'дегтева'),
                ('а.к.', 'соломкина'),
                ('в.', 'мегре'),
                ('анна', 'малышева'),
                ('александр', 'шапочкин'),
                ('алексей', 'широков')
            ON CONFLICT DO NOTHING");
        
        Execute.Sql(@"
            INSERT INTO books (title, isbn, quantity, price, publicationdate, publisher_id, genre_id,format_id)
            VALUES
                ('в огне аргентинского танго', '9785041575441', 25, 309, '2022-01-01', 1, 9, 2),
                ('адъютанты удачи', '9785041549435', 35, 249, '2021-01-01', 1, 10, 2),
                ('ворона, лисица и пицца. сказка', '9785353102434', 20, 647, '2022-01-01', 4, 11, 1),
                ('мой дневник с анкетами', '9785353102144', 28, 280, '2022-01-01', 4, 12, 8),
                ('энергия жизни', '9785171480035', 32, 389, '2022-01-01', 2, 13, 9),
                ('мастер охоты на единорога', '9785171386535', 31, 625, '2021-01-01', 2, 10, 1),
                ('обломки клана', '9785171341671', 27, 495, '2020-01-01', 2, 1, 1)
            ON CONFLICT DO NOTHING");
        
        Execute.Sql(@"
            INSERT INTO author_book (book_id, author_id)
            VALUES 
                (1,1),
                (2,2),
                (3,3),
                (4,4),
                (5,5),
                (6,6),
                (7,7),
                (7,8)
                ON CONFLICT DO NOTHING");
    }
}