using Conch.Goods;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDaprClient();
var ss = builder.Configuration.GetConnectionString("DbConnect");
builder.Services.AddDbContext<GoodsDBContext>(
                 options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();
var conn = app.Configuration["dbcontext"];
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllers();

app.Run();
