using DeltaApi.API.Middleware;

namespace DeltaApi.API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Configurar el pipeline de HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Delta API v1");
                c.RoutePrefix = string.Empty; // Swagger UI en la raíz
            });
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        
        // Middleware personalizado
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}


