using CQRS_Sample.Data.CommandHandlers;
using CQRS_Sample.Data.QueryHandlers;
using CQRS_Sample.Data.Repositories;
using CQRS_Sample.Queries;
using CQRS_Sample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Scan(scan => scan
    .FromCallingAssembly()
//Inject all of IQueryHandlers
    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
    .AsSelf()
    .WithScopedLifetime()
//Inject all of IAsyncQueryHandlers
    .AddClasses(classes => classes.AssignableTo(typeof(IAsyncQueryHandler<,>)))
    .AsSelf()
    .WithScopedLifetime()
//Inject all of ICommandHandlers
    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
    .AsSelf()
    .WithScopedLifetime()
//Inject all of IAsyncCommandHandlers
    .AddClasses(classes => classes.AssignableTo(typeof(IAsyncCommandHandler<>)))
    .AsSelf()
    .WithScopedLifetime()
//Inject all Services
    .AddClasses(classes => classes.AssignableTo<IAPIService>())
    .AsMatchingInterface()
    .WithScopedLifetime()
//Inject all Repositories
    .AddClasses(classes => classes.AssignableTo<IAPIRepository>())
    .AsMatchingInterface()
    .WithScopedLifetime()
    );
builder.Services.AddSingleton<IMediator, Mediator>();
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
