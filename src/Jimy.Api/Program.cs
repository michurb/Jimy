using System.Text.Json.Serialization;
using Jimy.Api.Data;
using Jimy.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//PG connection
builder.Services.AddDbContext<JimyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("JimyConnection")));

builder.Services.AddScoped<ExercisesService>();
builder.Services.AddScoped<TrainingSessionService>();



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseCookiePolicy();
app.Run();
