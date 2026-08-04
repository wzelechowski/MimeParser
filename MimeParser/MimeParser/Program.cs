using Microsoft.AspNetCore.Mvc;
using MimeParser.Exception;
using MimeParser.Payload;
using MimeParser.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<IProcessingService, ProcessingService>();
builder.Services.AddKeyedScoped<IParseService, InternalJsonParseService>(ParseType.INTERNAL_JSON);
builder.Services.AddKeyedScoped<IParseService, CsvParseService>(ParseType.CSV);

var app = builder.Build();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/v1/parse-content", (
    [FromBody] ParseRequest payload,
    HttpRequest request,
    [FromServices] IProcessingService processingService) => 
{
    if (!request.HasJsonContentType())
    {
        return Results.StatusCode(StatusCodes.Status415UnsupportedMediaType);
    }

    var result = processingService.ParseAndProcess(payload);

    if (!result.IsSuccess)
    {
        return Results.BadRequest(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation error",
            Detail = result.ErrorMessage
        });
    }

    return Results.Ok(result.Value);
});
    

app.Run();