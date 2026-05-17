var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var logger = context.RequestServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("GlobalExceptionHandler");

        context.Response.ContentType = "application/problem+json";

        if (exception is ArgumentException)
        {
            logger.LogWarning(exception, "Bad request while processing {Method} {Path}.", context.Request.Method, context.Request.Path);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await Results.Problem(exception.Message, statusCode: StatusCodes.Status400BadRequest).ExecuteAsync(context);
            return;
        }

        logger.LogError(exception, "Unhandled exception while processing {Method} {Path}.", context.Request.Method, context.Request.Path);
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await Results.Problem("An unexpected API error occurred.", statusCode: StatusCodes.Status500InternalServerError).ExecuteAsync(context);
    });
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(ReactCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();