using Benchmark.Calculator.Application;
using Benchmark.Calculator.Application.Features.Calculator;

using MediatR;

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/calculator/{numbers}/add", async ([FromServices] IMediator mediator, string? numbers) =>
{
    //Encoding...

    var command = new CalculatorCommand
    {
        Numbers = numbers
    };

    var response = await mediator.Send(command, new CancellationToken());

    return TypedResults.Ok(response);
})
.WithName("CalculatorAdd")
.WithOpenApi();

app.Run();

