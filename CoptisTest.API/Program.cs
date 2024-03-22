using CoptisTest.API;
using CoptisTest.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS to container needed to allow local access to API
builder.Services.AddCors(options => { options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); });

// Add local DB (CoptisTest) access
builder.Services.AddDbContextFactory<CoptisTestContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add a scoped UserRepository DB service to container
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Add a scoped AccessRepository DB service to container
builder.Services.AddScoped<IAccessRepository, AccessRepository>();
// Add a scoped ProductRepository DB service to container
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// Add a scoped OrderRepository DB service to container
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Add Console to access log
builder.Logging.AddConsole();

var app = builder.Build();

// Add logging info when app builded
app.Logger.LogInformation("App builded");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();