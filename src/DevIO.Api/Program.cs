using DevIO.Api.Configuration;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddDbContext<MeuDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        })
        .AddIdentityConfiguration(builder.Configuration)
        //.AddIdentityConfiguration(builder.Configuration)
        //builder.Services.AddAutoMapper(typeof(Program));
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddWebApiConfig()
        .Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        })
        .ResolveDependencies();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();

    app.UseWebApiConfig(app.Environment);

    app.Run();
}