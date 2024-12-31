using DotNetEnv;
using DTOs.MongoDb.Setting;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Online_Shopping.Extensions;
using Services;
using Services.MongoDB;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Configuration["ConnectionStrings:DbConnection"] = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext> (options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"),
    b => b.MigrationsAssembly("Online_Shopping")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Thay localhost:3000 bằng domain của frontend
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Nếu sử dụng cookie, bật tùy chọn này
    });
});

// Mongo
builder.Services.Configure<MongoDBSetting>(
    builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBClient>();

builder.Services.ConfigureCloudinary(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureRepository();
builder.Services.ConfigureService();
builder.Services.AddAutoMapper(typeof(AppMapper));

builder.Services.AddHttpClient();

builder.Services.ConfigureJWT(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication ();
app.UseAuthorization();

app.MapControllers();

app.Run();
