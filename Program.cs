using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<NZWalksDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.EnableTryItOutByDefault();
    });
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();


