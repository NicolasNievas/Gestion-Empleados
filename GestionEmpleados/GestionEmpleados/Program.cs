using FluentValidation;
using GestionEmpleados.CQRS.Commands;
using GestionEmpleados.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static GestionEmpleados.CQRS.Commands.Login;
using static GestionEmpleados.CQRS.Commands.NewUser;
using static GestionEmpleados.CQRS.Commands.PostCharge;
using static GestionEmpleados.CQRS.Commands.PostCity;
using static GestionEmpleados.CQRS.Commands.PostEmployee;
using static GestionEmpleados.CQRS.Commands.PutEmployee;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(config =>
{
    config.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddScoped<IValidator<PostChargeCommand>, PostChargeCommandValidator>();
builder.Services.AddScoped<IValidator<PostCityCommand>, PostCityCommandValidator>();
builder.Services.AddScoped<IValidator<PostEmployeeCommand>, PostEmployeeCommandValidator>(); 
builder.Services.AddScoped<IValidator<PutEmployeeCommand>, PutEmployeeCommandValidator>();
builder.Services.AddScoped<IValidator<NewUserCommand>, NewUserCommandValidator>();
builder.Services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();


builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
