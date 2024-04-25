using Api_test.Repositories;
using Api_test.Utilities;
using Api_test.Validators;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Api_test.Models.Request;

var builder = WebApplication.CreateBuilder(args);

var myConnection = "Server=localhost;Port=3306;Database=colab;Uid=root;Pwd=;charset=utf8;";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
builder.Services.AddDbContext<ApplicationContext>(options =>
        options.UseMySql(myConnection, serverVersion));


builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IValidator<DocumentoCadastroRequest>, DocumentoCadastroRequestValidator>();
builder.Services.AddTransient<IValidator<DocumentoPutRequest>, DocumentoPutRequestValidator>();
builder.Services.AddTransient<IValidator<ColaboradorPutRequest>, ColaboradorPutRequestValidator>();
builder.Services.AddTransient<IValidator<ColaboradorCadastroRequest>, ColaboradorCadastroRequestValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
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

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost");
app.UseAuthorization();

app.MapControllers();

app.Run();