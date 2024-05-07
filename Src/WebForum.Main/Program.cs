using Microsoft.AspNetCore.CookiePolicy;
using WebForum.Auth.Api.Authorization;
using WebForum.Auth.Api.Extensions;
using WebForum.Core.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWebForumAuth(builder.Configuration);
builder.Services.AddWebForumCore();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true));
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.SameAsRequest
});

app.MapGroup(String.Empty)
    .AddEndpointFilter<AuthorizationFilter>()
    .MapControllers();

await app.RunAsync();