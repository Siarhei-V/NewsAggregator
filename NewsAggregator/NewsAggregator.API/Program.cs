using Microsoft.EntityFrameworkCore;
using NewsAggregator.BLL;
using NewsAggregator.BLL.Interfaces;
using NewsAggregator.BLL.Services;
using NewsAggregator.DAL.EF;
using NewsAggregator.DAL.Entities;
using NewsAggregator.DAL.Interfaces;
using NewsAggregator.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//string connection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));

//builder.Services.AddScoped<IRepository<NewsSources>, EFNewsSourcesRepository>();
//builder.Services.AddScoped<IRepository<News>, EFNewsRepository>();

builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRssNewsUpdater, RssNewsUpdater>();
builder.Services.AddScoped<IRssNewsReader, RssNewsReader>();

//builder.Services.AddTransient<ApplicationContext>();



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
