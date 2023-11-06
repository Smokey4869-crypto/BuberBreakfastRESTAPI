using BuberBreakfast.Application;
using BuberBreakfast.Infrastructure;
using BuberBreakfast.Api.Middleware;
using BuberBreakfast.Api.Filter;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BuberBreakfast.Api.Errors;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, BuberBreakfastProblemDetailsFactory>();
}

var app = builder.Build();
{
    // app.UseMiddleware<ErrorHandlingMiddleware>(); using middleware for error handling
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}