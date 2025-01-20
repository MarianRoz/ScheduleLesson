using Microsoft.EntityFrameworkCore;
using ScheduleLesson.Services;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace ScheduleLesson
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IScheduleService, ScheduleService>();

            builder.Services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // ����� ���������� �� ���� ����� � appsettings.json
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // ������������ Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration) // ���� ������������ � appsettings.json
                .WriteTo.Console() // ��������� � �������
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // ��������� � ����
                .WriteTo.MSSqlServer(
                    connectionString: connectionString,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "Logs", // ����� ������� ��� ����
                        AutoCreateSqlTable = true // ����������� ��������� �������
                    })
                .CreateLogger();

            // ������ Serilog �� ����������
            builder.Host.UseSerilog();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            // ������ ��������� ������
            app.UseSerilogRequestLogging();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
