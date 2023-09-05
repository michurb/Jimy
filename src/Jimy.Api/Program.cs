using Jimy.Api.Data;
using Jimy.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//PG connection
builder.Services.AddDbContext<JimyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("JimyConnection")));
builder.Services.AddScoped<ExercisesService>();
builder.Services.AddScoped<TrainingSessionSerivce>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
