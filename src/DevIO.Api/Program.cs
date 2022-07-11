using DevIO.Api.Configuration;
using DevIO.Data.Context;
using HealthChecks.SqlServer;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<MeuDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentityConfiguration(builder.Configuration);
//.AddIdentityConfiguration(builder.Configuration)
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddWebApiConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddLoggingConfig(builder.Configuration);

builder.Services.ResolveDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseWebApiConfig(app.Environment);
app.UseSwaggerConfig(apiVersionDescriptionProvider);
app.UseLoggingConfiguration();

app.Run();
