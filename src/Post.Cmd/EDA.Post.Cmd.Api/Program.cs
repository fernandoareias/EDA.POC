using EDA.Core.Domain;
using EDA.Core.Handlers;
using EDA.Core.Infraestructure;
using EDA.Post.Cmd.Infraestructure.Config;
using EDA.Post.Cmd.Infraestructure.Handlers;
using EDA.Post.Cmd.Infraestructure.Repositories;
using EDA.Post.Cmd.Infraestructure.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<typeof(IEventSourcingHandler<>), EventSourcingHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
