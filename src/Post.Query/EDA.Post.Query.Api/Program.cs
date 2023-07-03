using Confluent.Kafka;
using EDA.Post.Query.Api.Queries;
using EDA.Post.Query.Api.Queries.Handlers;
using EDA.Post.Query.Domain.Repositories;
using EDA.Post.Query.Infraestructure.Consumers;
using EDA.Post.Query.Infraestructure.Data;
using EDA.Post.Query.Infraestructure.Dispatchers;
using EDA.Post.Query.Infraestructure.Handlers;
using EDA.Post.Query.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Action<DbContextOptionsBuilder> configureDbContext = (o => o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IEventHandler, EDA.Post.Query.Infraestructure.Handlers.EventHandler>();
builder.Services.AddScoped<IQueryHandler, QueryHandler>();

//
var queryHandler = builder.Services.BuildServiceProvider().GetService<QueryHandler>();
var dispatcher = new QueryDispatcher();
dispatcher.RegisterHandler<FindAllPostsQuery>(queryHandler.HandleAsync);

builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));
builder.Services.AddHostedService<ConsumerHostedService>();

// Cria database e tabelas
var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
dataContext.Database.EnsureCreated();

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
