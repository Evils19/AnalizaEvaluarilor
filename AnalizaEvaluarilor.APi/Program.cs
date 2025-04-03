using System.Text;
using AnalizaEvaluarilor.Application.Service;
using AnalizaEvaluarilor.Domain.Entity.User;
using AnalizaEvaluarilor.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    var suymmetricSecurityKey = new SymmetricSecurityKey(builder.Configuration.GetValue<string>("Jwt:Secret").Select(c => (byte)c).ToArray());

    option.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = suymmetricSecurityKey,
        ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true
    };
});

string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
string connectionConfig = environment switch
{
    "Staging" => "StagingConnection",
    "Production" => "DefaultConnection",
    _ => "LocalConnection"
};

var connectionString = builder.Configuration.GetConnectionString("DockerConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString),
    ServiceLifetime.Scoped);

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();