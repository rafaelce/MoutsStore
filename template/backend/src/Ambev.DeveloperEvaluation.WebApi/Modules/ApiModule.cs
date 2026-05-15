using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Modules;

public static class ApiModule
{
    public static WebApplicationBuilder AddApiModule(this WebApplicationBuilder builder)
    {
        builder.AddBasicHealthChecks();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDatabaseModule(builder.Configuration);
        builder.Services.AddJwtAuthentication(builder.Configuration);
        builder.RegisterDependencies();
        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(ApplicationLayer).Assembly,
                typeof(Program).Assembly);
        });
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return builder;
    }

    public static WebApplication UseApiModule(this WebApplication app)
    {
        app.UseMiddleware<ValidationExceptionMiddleware>();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseBasicHealthChecks();
        app.MapControllers();
        return app;
    }

}
