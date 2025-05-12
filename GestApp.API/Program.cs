using Microsoft.EntityFrameworkCore;
using GestApp.Data;
using GestApp.Data.Repositories;
using GestApp.Business.Services;

var builder = WebApplication.CreateBuilder(args);

//  cadena de conexión
builder.Services.AddDbContext<GestAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<ProductoService>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
