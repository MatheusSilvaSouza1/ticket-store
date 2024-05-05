using API.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDependecyInjector();
builder.Services.RegisterDatabase();
builder.Services.RegisterMenssageBus();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
