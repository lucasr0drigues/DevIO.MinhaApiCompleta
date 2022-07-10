using DevIO.Api.Configuration;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddEndpointsApiExplorer()
        .AddDbContext<MeuDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        })
        .AddIdentityConfiguration(builder.Configuration)
        //.AddIdentityConfiguration(builder.Configuration)
        //builder.Services.AddAutoMapper(typeof(Program));
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddWebApiConfig()
        .AddSwaggerConfig()
        .Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        })
        .ResolveDependencies();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.

    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.MapControllers();

    app.UseWebApiConfig(app.Environment);
    app.UseSwaggerConfig(apiVersionDescriptionProvider);

    app.Run();
}