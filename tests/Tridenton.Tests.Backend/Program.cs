using Microsoft.AspNetCore.Mvc;
using Tridenton.Internal.Core;
using Tridenton.Internal.Core.CQRS;
using Tridenton.Tests.Backend.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTridentonCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/test", async ([FromServices] IOrchestrator orchestrator) =>
{
    var start = DateTimeOffset.UtcNow;
    
    var response = await orchestrator.InvokeAsync<GetTestDataRequest, GetTestDataResponse>(new());

    if (response.Failed)
    {
        return Results.StatusCode((int)response.Error!.Code);
    }
    
    var value = response.Value;
    
    value.Start = start;

    return Results.Ok(value);
});

app.MapPost("/test", async (
    [FromBody] TestCommandRequest request,
    [FromServices] IOrchestrator orchestrator) =>
{
    var response = await orchestrator.InvokeAsync(request);
    
    if (response.Failed)
    {
        return Results.StatusCode((int)response.Error!.Code);
    }

    return Results.Ok();
});

app.Run();