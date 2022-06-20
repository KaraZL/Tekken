using CharactersAPI.Data;
using CharactersAPI.Models;
using CharactersAPI.Policies;
using CharactersAPI.Repository;
using CharactersAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Entity Framework
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:Database"]);
});

//DI
builder.Services.AddScoped<ISqlRepository<Character>, CharacterSqlRepository>();
builder.Services.AddScoped<ICharacterService, CharacterService>();

//Polly
builder.Services.AddSingleton<ClientPolicy>();

//Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Secure API
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Audience = builder.Configuration["AzureAD:ResourceId"];
        options.Authority = $"{builder.Configuration["AzureAD:InstanceId"]}{builder.Configuration["AzureAD:TenantId"]}";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//Secure API
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app);

app.Run();
