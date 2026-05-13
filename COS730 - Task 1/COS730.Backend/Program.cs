using COS730.Backend.Controllers;
using COS730.Backend.Services;
using SQLitePCL;
Batteries.Init();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ReviewManager>();
builder.Services.AddScoped<EvalutionManager>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddSingleton<Database>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();