using Microsoft.EntityFrameworkCore;
using WebApi.DataContext;
using WebApi.DataContext.Data;
using WebApi.Helpers;
using WebApi.Services;


var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
{
    var services = builder.Services;

    services.AddCors();
    services.AddControllers();
    services.AddMvc();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ICuentaBancariaService, CuentaBancariaService>();
    services.AddScoped<ITransferenciaBancariaService, TransaccionesBancariasService>();


    services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "Testing"));

    services.AddAuthorization();
}

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


{
    //1. Find the service layer within our scope.
    using (var scope = app.Services.CreateScope())
    {
        //2. Get the instance of AppDbContext in our services layer
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();

        //3. Call the DataGenerator to create sample data
        DbInitializer.Initialize(services);
    }
}

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.UseAuthorization();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

app.Run("http://localhost:4000");