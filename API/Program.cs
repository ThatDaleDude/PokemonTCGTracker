using API.Context;
using Microsoft.EntityFrameworkCore;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("AllowSpecificOrigin", policy =>
{
    policy.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Value!)
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<PokemonApiClient>(_ => new PokemonApiClient(builder.Configuration.GetSection("PokemonApiKey").Value!));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.UseHttpsRedirection();

app.Run();