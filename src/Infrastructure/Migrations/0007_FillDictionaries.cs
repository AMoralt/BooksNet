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
                ('Твердая бумажная'),
                ('Мягкая бумажная'),
                ('Электронная книга'),
                ('Аудио-книга'),
                ('Журнал'),
                ('Газета'),
                ('Комикс'),
                ('Интегральная'),
                ('Мягкая глянцевая')
            ON CONFLICT DO NOTHING");
        
        Execute.Sql(@"
            INSERT INTO genres (name)
            VALUES 
                ('Фантастика'),
                ('Драма'),
                ('Экшн'),
                ('Приключение'),
                ('Роман'),
                ('Мистика'),
                ('Хоррор'),
                ('Проза'),
                ('Романтика'),
                ('Детектив'),
                ('Сказки'),
                ('Дневник'),
                ('Эзотерика')
            ON CONFLICT DO NOTHING");
        
        Execute.Sql(@"
            INSERT INTO publishers (name)
            VALUES
                ('Эксмо'),
                ('АСТ'),
                ('Азбука'),
                ('Росмэн')
            ON CONFLICT DO NOTHING");

        Execute.Sql(@"
            INSERT INTO authors (firstname, lastname)
            VALUES
                ('Татьяна', 'Алюшина'),
                ('Валерия', 'Вербинина'),
                ('Валентина', 'Дегтева'),
                ('А.К.', 'Соломкина'),
                ('В.', 'Мегре'),
                ('Анна', 'Малышева'),
                ('Александр', 'Шапочкин'),
                ('Алексей', 'Широков')
            ON CONFLICT DO NOTHING");
        
        Execute.Sql(@"
            INSERT INTO books (title, isbn, quantity, price, publicationdate, publisher_id, genre_id,format_id)
            VALUES
                ('В огне аргентинского танго', '9785041575441', 25, 309, '2022-01-01', 1, 9, 2),
                ('Адъютанты удачи', '9785041549435', 35, 249, '2021-01-01', 1, 10, 2),
                ('Ворона, Лисица и пицца. Сказка', '9785353102434', 20, 647, '2022-01-01', 4, 11, 1),
                ('Мой дневник с анкетами', '9785353102144', 28, 280, '2022-01-01', 4, 12, 8),
                ('Энергия жизни', '9785171480035', 32, 389, '2022-01-01', 2, 13, 9),
                ('Мастер охоты на единорога', '9785171386535', 31, 625, '2021-01-01', 2, 10, 1),
                ('Обломки клана', '9785171341671', 27, 495, '2020-01-01', 2, 1, 1)
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