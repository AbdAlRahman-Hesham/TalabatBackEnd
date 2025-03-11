using E_Commerce.APIs.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddCors();

var app = builder.Build();

app.UseExceptionHandlingMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(O=>O.SwaggerEndpoint("/openapi/v1.json","Talabat Api"));

}
await app.UseUpdateDataBase();
await app.UseSeeding();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

//app.UseStaticFiles();
app.UseCors(op =>
{
    op.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.StartRedisServer();
app.Run();