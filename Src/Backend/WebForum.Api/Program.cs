using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using WebForum.Domain.Models;
using WebForum.Domain.Models.AuthModels;
using WebForum.Domain.Models.SpaceModels;
using WebForum.Domain.Models.UserModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Space>("Spaces");
modelBuilder.EntitySet<User>("User");
modelBuilder.EntitySet<Auth>("Auth");

builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
        "api/odata",
        modelBuilder.GetEdmModel())
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseRouting();
await app.RunAsync();