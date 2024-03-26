using Microsoft.EntityFrameworkCore;
using TallerApi.Data.DefaultData;
using TallerApi.DataAccess;
using TallerApi.Interface;
using TallerApi.Services;
//using TallerApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//a ---------------------------------------------------
var conn = builder.Configuration.GetConnectionString("AppConnection");
//Service layer
builder.Services.AddDbContext<TallerDbContext>(options => options.UseSqlServer(conn));
//builder.Services.AddScoped<EmployeeService>();
//builder.Services.AddScoped<ICSVService, CSVService>();

var app = builder.Build();

//------------------------------------------
//DatabaseInitializer.Seed(app);


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
