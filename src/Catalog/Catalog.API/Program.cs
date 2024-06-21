using API.Config;
using Infra;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.RegisterDependencyInjector();
builder.Services.RegisterDatabase(builder.Configuration);
builder.Services.RegisterMessageBus(builder.Configuration);
builder.Services.AddSwaggerConfiguration();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services?.GetService<IServiceScopeFactory>()?.CreateScope();
var context = scope?.ServiceProvider.GetRequiredService<Context>();
context?.Database.Migrate();

app.Run();
