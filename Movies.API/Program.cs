using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies.API.Configurations;
using Movies.API.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieContext>(options =>
{
    options.UseInMemoryDatabase("Movie");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});


var identityGroupAccess = builder.Configuration
    .GetSection("IdentityGroupAccess")
    .Get<IdentityGroupAccess>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.RequireHttpsMetadata = false;
        options.Audience = identityGroupAccess.Audience;
        options.Authority = identityGroupAccess.Authority;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
        };
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

app.UseCors("DevCorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
