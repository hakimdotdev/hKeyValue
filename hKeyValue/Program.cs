using StackExchange.Redis;
using Testcontainers.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var redisContainer = new RedisBuilder()
  .WithImage("redis:7.0")
  .Build();
await redisContainer.StartAsync();

builder.Services.AddSingleton(cfg =>
{
    return ConnectionMultiplexer.Connect(redisContainer.GetConnectionString());
});

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
