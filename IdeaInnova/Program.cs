//RUDRA PATEL
//JIYA PANDIT
//KOMALPREET KAUR
//DRASTI PATEL

using IdeaInnova.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();   //Enables MVC controllers and Razor views
builder.Services.AddSession(); // Add session support

// Enable Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();   //Enables endpoints discovery
builder.Services.AddSwaggerGen();   //Registers the Swagger generator for producing API docs

// Configure Database Connection
builder.Services.AddDbContext<IdeaInnovaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  //Retrieves the connection string from appsettings.json under "DefaultConnection"

var app = builder.Build();  

// Enable Swagger UI in Development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");   //Configures exception handling to use a custom error page
    app.UseHsts();   //Adds HTTP Strict Transport Security (HSTS) headers for security
}

app.UseHttpsRedirection();   //Redirects HTTP requests to HTTPS
app.UseStaticFiles();   //Enables serving static files from wwwroot folder
app.UseRouting();   //Adds routing capabilities to middleware piprline
app.UseSession(); // Enable session
app.UseAuthorization();  //Adds authorization middleware

//Define the default route pattern for MVC controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"  //When URL is root, it defaults to the AccountController's Login Action
);

app.Run();

public partial class Program { }
