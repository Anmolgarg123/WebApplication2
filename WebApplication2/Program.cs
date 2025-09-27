using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

// CORS policy for Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular dev URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("AllowAngularApp");

// Serve Angular files from wwwroot
app.UseDefaultFiles();   // serves index.html by default
app.UseStaticFiles();    // serves JS/CSS/assets

app.UseHttpsRedirection();
app.UseAuthorization();

// Map API endpoints
app.MapControllers();
app.MapHealthChecks("/health");

// Optional: fallback to index.html for Angular routes
app.MapFallbackToFile("index.html");

app.Run();
