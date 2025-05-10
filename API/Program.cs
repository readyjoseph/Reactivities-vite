using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
//There are 2 main sections here. Services and Middleware.

// SECTION 1 Add services to the container.
// Any class to be used in the API controller can be added here using DI.
//This way we leave it to the framework to manage the lifecycle of the class and we can inject it into the controller constructor.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
//we will use it by DI. as it is a service.
//Note. We cannot inject this service into the program class. We can only inject it into the controller class.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); // Use SQLite as the database provider.
});
//for CORS
builder.Services.AddCors();
var app = builder.Build();
/////------------------------------------------------------//////
// SECTION 2 Configure the HTTP request pipeline.
// Requests go through middleware before reaching the controller.
// Middleware is a class that has a method called InvokeAsync that takes an HttpContext as a parameter and returns a Task.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

app.UseHttpsRedirection();

//app.UseAuthorization();
//Add cors before MapControllers middleware
app.UseCors(options => options.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:3000", "https://localhost:3000")); // Allow any header and method from the specified origin.

app.MapControllers(); // Map the controllers to the app pipeline. This is where the routing happens.
//app.MapGet("/", () => "Hello World!"); // This is a simple endpoint that returns a string. It is not used in this example.

//To use AppDbContext in Program.cs, we need to use Service Locator Pattern.
using var scope = app.Services.CreateScope(); // Create a scope for the database context.
var services = scope.ServiceProvider; // Get the service provider from the scope.
try
{
    var context = services.GetRequiredService<AppDbContext>(); // Get the database context from the service provider.
    await context.Database.MigrateAsync(); // Apply any pending migrations to the database.
    await DbInitializer.SeedData(context); // Seed the database with initial data.
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>(); // Get the logger from the service provider.
    logger.LogError(ex, "An error occurred during migration"); // Log any errors that occur during migration.
}

app.Run();
