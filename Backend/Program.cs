using Backend.Automappers;
using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Backend.Services;
using Backend.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddSingleton<IPeopleService, People2Service>();
//builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService");
//builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("people2Service");

builder.Services.AddKeyedSingleton<IRandomService, RandomService>("singleton");
builder.Services.AddKeyedScoped<IRandomService, RandomService>("scoped");
builder.Services.AddKeyedTransient<IRandomService, RandomService>("transient");
builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddKeyedScoped<ICommonService<BeerSelectDto, BeerInsertDto, BeerUpdateDto>, BeerService>("BeerService");

//HttpClient
builder.Services.AddHttpClient<IPostsService, PostsService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPosts"]);
});

//Store Context
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

//Repository
builder.Services.AddScoped<IRepository<Beer>, BeerRepository>();

//AutoMappers  
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Validators
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

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
