using Microsoft.EntityFrameworkCore;
using TmkStore.Application.Services;
using TmkStore.Core.Abstractions;
using TmkStore.DataAccess;
using TmkStore.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TmkStoreDbContext>(
        options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(TmkStoreDbContext)));
        }
    );

// Register Repositories
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<INomenclatureRepository, NomenclatureRepository>();
builder.Services.AddScoped<IPipeTypeRepository, PipeTypeRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IRemnantRepository, RemnantRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();

// Register Services
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<INomenclatureService, NomenclatureService>();
builder.Services.AddScoped<IPipeTypeService, PipeTypeService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IRemnantService, RemnantService>();
builder.Services.AddScoped<IPriceService, PriceService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x =>
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:3000");
    x.WithMethods().AllowAnyMethod();
});

app.Run();
