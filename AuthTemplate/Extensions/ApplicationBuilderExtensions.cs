using AuthTemplate.Infrastructure.Persistence;

namespace AuthTemplate.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        dbContext.Database.EnsureCreated();
        return app;
    }

    public static WebApplication UsePresentation(this WebApplication app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthTemplate API v1");
                options.RoutePrefix = "swagger";
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
