using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebForum.Frontend;
using WebForum.Frontend.HttpClients;
using WebForum.Frontend.Services;
using WebForum.Frontend.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddHttpClient<AuthHttpClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});
builder.Services.AddHttpClient<ProfileHttpClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});
builder.Services.AddHttpClient<PostHttpClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

await builder.Build().RunAsync();