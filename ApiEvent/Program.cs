using Api2.DbContexts;
using Api2.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(
    logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    });
builder.Services.AddDbContext<EventContext>(dbContextOptions => dbContextOptions.UseSqlServer
(builder.Configuration["Connectionstring:CityInfoConnectionString"]));
// Add services to the container.
builder.Services.AddScoped<IEventRepository,EventRepository>();
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


app.UseRouting();

app.UseAuthentication();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();

