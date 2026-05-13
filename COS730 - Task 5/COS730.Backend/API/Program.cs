using COS730.Backend.API;
using COS730.Backend.Application;
using COS730.Backend.Infrastructure;
using SQLitePCL;
Batteries.Init();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddScoped<EvaluationService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ReviewerService>();
builder.Services.AddScoped<SubmissionService>();
builder.Services.AddScoped<ValidationService>();

builder.Services.AddScoped<ReviewerRepository>();
builder.Services.AddScoped<ScoreRepository>();
builder.Services.AddScoped<SubmissionRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();