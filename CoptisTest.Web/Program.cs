using CoptisTest.Web.Components;
using CoptisTest.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add a scoped UserRepository DB service to container
builder.Services.AddSingleton<ISessionRepository, SessionRepository>();

// Add Console to access log
builder.Logging.AddConsole();

builder.Services.AddBlazorBootstrap();

builder.Services.AddHttpClient<IUserRepository, UserRepository>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7175/");
});
builder.Services.AddHttpClient<IAccessRepository, AccessRepository>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7175/");
});
builder.Services.AddHttpClient<IProductRepository, ProductRepository>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7175/");
});
builder.Services.AddHttpClient<IOrderRepository, OrderRepository>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7175/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
