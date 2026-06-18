using Api.Exceptions;
using Api.Extensions;
using Application;
using Application.Extensions;
using Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHealthChecksConfiguration();

builder.Services.AddExceptionHandler<CustomExceptionHandler>()
                .AddProblemDetails();

var app = builder.Build();

app.MapApiEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTheme(ScalarTheme.DeepSpace);
        options.WithDefaultHttpClient(
            ScalarTarget.CSharp,
            ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseExceptionHandler();


app.Run();