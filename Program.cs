using BackEndAPI.Models;
using BackEndAPI.Services.Contractors;
using BackEndAPI.Services.Implementation;
using BackEndAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BackEndAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DBEmployeeContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            builder.Services.AddCors(options => options.AddPolicy("PolicyToApp", app =>
              {
                  app.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
              }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.UseCors("PolicyToApp");

            app.MapControllers();

            app.Run();
        }
    }
}