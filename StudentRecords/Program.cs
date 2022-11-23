using Microsoft.EntityFrameworkCore;
using StudentRecords.Data;
using StudentRecords.Service;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("StudentDbContext");
builder.Services.AddDbContext<StudentDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IStudentDbContext, StudentDbContext>();
builder.Services.AddScoped<ImessageProducer>();
builder.Services.AddScoped<RabbitMQProducer>();

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

app.MapControllers();

app.Run();
