using Api_test.Models;
using Api_test.Repositories;
using JsonPatchSample;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);    

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySql(
        "server=localhost;port=3306;initial catalog=ColaboradorAPI;uid=root;pwd=Tb@159753",
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql")));

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IValidator<ColaboradorModel>, ColaboradorValidatorModel>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();