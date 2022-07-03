using CQRS_Sample.Data.CommandHandlers;
using CQRS_Sample.Data.DbContexts;
using CQRS_Sample.Data.QueryHandlers;
using CQRS_Sample.Data.QueryHandlers.Customers;
using CQRS_Sample.Data.Repositories;
using CQRS_Sample.DTOs.Customers;
using CQRS_Sample.Queries;
using CQRS_Sample.Queries.Customers;
using CQRS_Sample.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var path = AppContext.BaseDirectory;
path = Path.GetFullPath(Path.Combine(path, @"..\..\..\"));
AppDomain.CurrentDomain.SetData("DataDirectory", path);
builder.Services.AddDbContext<MainDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlite"));
});
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddTransient<IMediator, Mediator>();

builder.Services.Scan(scan => scan
    .FromCallingAssembly()
//Inject all of IQueryHandlers
    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
    .AsImplementedInterfaces()
    .WithTransientLifetime()
//Inject all of IAsyncQueryHandlers
    .AddClasses(classes => classes.AssignableTo(typeof(IAsyncQueryHandler<,>)))
    .AsImplementedInterfaces()
    .WithTransientLifetime()
//Inject all of ICommandHandlers
    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
    .AsImplementedInterfaces()
    .WithTransientLifetime()
//Inject all of IAsyncCommandHandlers
    .AddClasses(classes => classes.AssignableTo(typeof(IAsyncCommandHandler<>)))
    .AsImplementedInterfaces()
    .WithTransientLifetime()
//Inject all Services
    .AddClasses(classes => classes.AssignableTo<IAPIService>())
    .AsMatchingInterface()
    .WithScopedLifetime()
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
