// ============================================================================
// File: Program.cs
// Author: Utkarsh Tripathi
// Created On: 16-Aug-2025
// Description: Main entry point for the AI Blog Generator ASP.NET Core application.
//              Configures services, middleware, and routes.
// ============================================================================

using AI_Blog_Generator.Repository;

var builder = WebApplication.CreateBuilder(args);

#region Services Configuration
// Register BlogRepository as the implementation for IBlogRepository
builder.Services.AddScoped<IBlogRepository, BlogRepository>();

// Add support for controllers and views (MVC)
builder.Services.AddControllersWithViews();
#endregion

var app = builder.Build();

#region Middleware Configuration
// Serve static files (CSS, JS, images, etc.)
app.UseStaticFiles();

// Enable routing
app.UseRouting();

// Enable authorization middleware (optional, for future use)
app.UseAuthorization();
#endregion

#region Route Configuration
// Define default route: BlogController -> Index action
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");
#endregion

// Run the application
app.Run();
