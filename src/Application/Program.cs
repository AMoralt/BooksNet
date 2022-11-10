using Domain.AggregationModels.Book;
using EmptyProjectASPNETCORE;
using EmptyProjectASPNETCORE.Migrations;
using FluentMigrator.Runner;
using MediatR;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseConnectionOptions>(builder.Configuration.GetSection("DatabaseConnectionOptions"));
builder.Services.AddMediatR(typeof(Program), typeof(DatabaseConnectionOptions));
builder.Services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
builder.Services.AddScoped<IChangeTracker,ChangeTracker>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IRepository<Genre>, GenreRepository>();
builder.Services.AddScoped<IRepository<Publisher>, PublisherRepository>();
builder.Services.AddScoped<IRepository<BookFormat>, BookFormatRepository>();
builder.Services.AddScoped<IRepository<Author>, AuthorRepository>();

var connectionString = builder.Configuration["DatabaseConnectionOptions:ConnectionString"];

builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(r => r
        .AddPostgres()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(BookTable).Assembly)
        .For.Migrations());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    using var scope = ((IApplicationBuilder) app).ApplicationServices.CreateScope();
    var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
    migrator.MigrateUp();
}

app.MapControllers();

await app.RunAsync();

