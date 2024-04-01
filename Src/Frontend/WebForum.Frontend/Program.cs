using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WebForum.Frontend;
using WebForum.Frontend.HttpClients;
using WebForum.Frontend.Repositories;
using WebForum.Frontend.Repositories.Interfaces;
using WebForum.Frontend.Services;
using WebForum.Frontend.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
Console.WriteLine(builder.HostEnvironment.BaseAddress);
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5129/") });
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);
    options.UseReduxDevTools();
});

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddHttpClient<AuthHttpClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5129/");
});

await builder.Build().RunAsync();