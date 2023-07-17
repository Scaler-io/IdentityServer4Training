using Microsoft.EntityFrameworkCore;
using Movies.API.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieContext>(options =>
{
    options.UseInMemoryDatabase("Movie");
});

var app = builder.Build();
var serviceProvider = app.Services;

using (var scope = serviceProvider.CreateScope())
{
    var movieContext = scope.ServiceProvider.GetRequiredService<MovieContext>();
    MovieContextSeed.Seed(movieContext);
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
