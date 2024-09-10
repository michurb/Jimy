using Jimy.Business;
using Jimy.Data.Data;
using Jimy.Data.Interfaces;
using Jimy.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:7107", "http://localhost:5284")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.UseCors("AllowBlazorOrigin");
app.Run();