// //using Org.BouncyCastle.Crypto.Utilities;
// //using DirectoryBlock.Context;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Options;
// using Microsoft.OpenApi.Models;
// using System.Security.Claims;
// using System.Text.Encodings.Web;
// using AnnuaireAPI;
//
//
// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
//
// builder.Services.AddSwaggerGen(options =>
// {
//     //var xmlFilename = "AnnuaireAPI.xml";
//     //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
//     //options.SwaggerDoc("v1", new OpenApiInfo { Title = "DirectoryBlock.API", Version = "v1" });
//
// });
// var connectionString = builder.Configuration.GetConnectionString("DatabaseConnectionSqlLite");
//
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlite(connectionString));
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(options =>
//     {
//         options.SwaggerEndpoint("/swagger/v1/swagger.json", "AnnuaireAPI v1");
//         options.RoutePrefix = string.Empty;
//     });
// }
//
// app.UseHttpsRedirection();
// app.UseAuthorization();
// app.MapControllers();
//
//
// app.Run();







// using AnnuaireAPI.Controllers;
// using Microsoft.EntityFrameworkCore;
//
// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddControllers().AddApplicationPart(typeof(EmployeeController).Assembly);
// //builder.Services.AddControllers().AddNewtonsoftJson(); // Optionnel mais utile
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseHttpsRedirection();
//
// app.UseSwagger();
// app.UseSwaggerUI(options =>
// {
//     options.SwaggerEndpoint("/swagger/v1/swagger.json", "AnnuaireAPI v1");
//     options.RoutePrefix = string.Empty;
// });
// app.UseRouting();
//
// app.MapControllers();
// app.Run();



using AnnuaireAPI.Controllers;
using AnnuaireAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnectionSqlLite");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseDeveloperExceptionPage();

app.MapControllers();
app.Run();

